namespace PcStoreWpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;

    public class MyIoc : SimpleIoc, IServiceLocator
    {
        public static MyIoc Instance { get; private set; } = new MyIoc();
    }
}
