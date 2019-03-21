using AMC.Core.Abstractions.DataProvider;

namespace AMC.Core.Abstractions.QuantumModel.QuantumAdapter
{
    interface IQuantumDeployment<T> where T : IQuant
    {
        void Install(IDataStorage dataStorage);

        void Uninstall(IDataStorage dataStorage);

        bool IsInstalled(IDataStorage dataStorage);
    }
}
