using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EShope.Services.Data.Models;
using EShope.Services.Infra;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace EShope.Services.Data.Imp
{
    public class ProductService : IProductService
    {
        IAPIConsumer _api;
        IMobileServiceClient _client;
        IMobileServiceSyncTable<Product> _productTable;
        public ProductService(IAPIConsumer api)
        {
            _api = api;
            _client = new MobileServiceClient(api.DefaultEndPoint);
            //InitLocalStoreAsync();
            var store = new MobileServiceSQLiteStore("eshopelocaldb.db");
            store.DefineTable<Product>();

            _client.SyncContext.InitializeAsync(store);
            _productTable = _client.GetSyncTable<Product>();
        }
        //private async Task InitLocalStoreAsync()
        //{
        //    // new code to initialize the SQLite store
        //    string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "eshopelocaldb.db");

        //    if (!File.Exists(path))
        //    {
        //        File.Create(path).Dispose();
        //    }

        //    var store = new MobileServiceSQLiteStore(path);
        //    store.DefineTable<Product>();

        //    // Uses the default conflict handler, which fails on conflict
        //    await _client.SyncContext.InitializeAsync(store);
        //}

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                //await this._client.SyncContext.PushAsync();
                //_productTable.DeleteAsync();
                //var productList = await _productTable.ToListAsync();
                //productList.ForEach(p => _productTable.DeleteAsync(p));
                var productTableQuery = this._productTable.CreateQuery();
                await this._productTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "availableProducts", productTableQuery);
            }

            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }
            catch (Exception ex)
            {

            }
            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }

        public async Task<List<Product>> GetProductsAsync(bool syncItems = false)
        {
            try
            {
                if (syncItems)
                {
                    await this.SyncAsync();
                }
                //var productQuery = _productTable;//.Where(p => p.AvailableQuantity > 1);
                var productList = await _productTable.ToListAsync();              
                productList.ForEach(p => { p.ThumnailURL = _api.DefaultEndPoint + "/Images/" + p.ThumnailURL; });
                return productList;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        //public async Task<List<Product>> GetProducts()
        //{
        //    return await Task.Run(() => new List<Product> {
        //        new Product{
        //            Id=1,
        //            Name ="TV",
        //            Description ="1 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
        //            Price= 30},
        //        new Product{
        //            Id=2,
        //            Name ="Mobile",
        //            Description ="2 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
        //            Price= 50},
        //    });
        //}
    }
}
