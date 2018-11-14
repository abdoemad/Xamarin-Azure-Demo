using AutoMapper;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace EShope.API.Models.DomainManagers
{
    internal class ComplexTypeDomainManager<TData, TModel, TKey> : MappedEntityDomainManager<TData, TModel>
            where TData : class, ITableData
            where TModel : class
    {
        public ComplexTypeDomainManager(DbContext context, HttpRequestMessage request/*, ApiServices services*/)
            : base(context, request/*, services*/)
        {
        }

        public override SingleResult<TData> Lookup(string id)
        {
            TKey tkey = GetKey<TKey>(id);
            TModel model = this.Context.Set<TModel>().Find(tkey);
            List<TData> result = new List<TData>();
            if (model != null)
            {
                result.Add(Mapper.Map<TData>(model));
            }

            return SingleResult.Create(result.AsQueryable());
        }

        public override async Task<TData> UpdateAsync(string id, Delta<TData> patch)
        {
            TKey tkey = GetKey<TKey>(id);
            TModel model = await this.Context.Set<TModel>().FindAsync(tkey);
            if (model == null)
            {
                throw new HttpResponseException(this.Request.CreateNotFoundResponse());
            }

            TData data = Mapper.Map<TData>(model);

            patch.Patch(data);
            // Need to update reference types too.
            foreach (var pn in patch.GetChangedPropertyNames())
            {
                Type t;
                if (patch.TryGetPropertyType(pn, out t) && t.IsClass)
                {
                    object v;
                    if (patch.TryGetPropertyValue(pn, out v))
                    {
                        data.GetType().GetProperty(pn).GetSetMethod().Invoke(data, new object[] { v });
                    }
                }
            }

            Mapper.Map<TData, TModel>(data, model);
            await this.SubmitChangesAsync();

            return data;
        }

        public override Task<bool> DeleteAsync(string id)
        {
            TKey tkey = GetKey<TKey>(id);
            return this.DeleteItemAsync(tkey);
        }
    }
}