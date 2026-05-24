// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PPSDotNetInternshipTraining.EFCoreSample2;

EFCoreSample2 ef = new EFCoreSample2();
ef.Create();
ef.Edit();
ef.Update();
ef.Delete();
ef.Read();