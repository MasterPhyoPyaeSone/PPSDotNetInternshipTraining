// See https://aka.ms/new-console-template for more information
using PPSDotNetInternshipTraining.EFCoreSample;


EFCoreSample ef = new EFCoreSample(new AppDbContext());
ef.Read();
ef.Create();
ef.Edit();
ef.Update();
ef.Delete();
ef.Read();
