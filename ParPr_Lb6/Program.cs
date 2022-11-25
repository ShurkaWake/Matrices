using ParPr_Lb6;

string arg = args.Length > 0 ? args[0] : "";

switch (arg.ToLower())
{
    case "numadd":
        Utils.NumberMatrixAddTest(1 << 14, 8);
        break;
    case "nummul":
        Utils.NumberMatrixMultiplyTest(1 << 11, 8);
        break;
    case "bitadd":
        Utils.BoolMatrixAddTest(1 << 15, 8);
        break;
    case "bitmul":
        Utils.BoolMatrixMultiplyTest(1 << 12, 8);
        break;
    default:
        Utils.NumberMatrixAddTest(1 << 14, 8);
        GC.Collect();
        Utils.NumberMatrixMultiplyTest(1 << 11, 8);
        GC.Collect();
        Utils.BoolMatrixAddTest(1 << 15, 8);
        GC.Collect();
        Utils.BoolMatrixMultiplyTest(1 << 12, 8);
        break;
}