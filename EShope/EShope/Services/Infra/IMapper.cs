using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Services.Infra
{
    public interface IMapper
    {
        void Initialize(Dictionary<Type, Type> mapConfiguration);

        R Map<S,R>(S input);

    }
}
