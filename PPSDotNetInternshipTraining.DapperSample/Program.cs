// See https://aka.ms/new-console-template for more information
using PPSDotNetInternshipTraining.DapperSample;

class Program
{
    static void Main(string[] args)
    {
        DapperSample dapperSample = new DapperSample();
        dapperSample.Read();
        dapperSample.Edit();
        dapperSample.Create();
        dapperSample.Update();
        dapperSample.Delete();
        dapperSample.Read();
    }
}
