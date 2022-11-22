using ParPr_Lb6;

int w, h;

int length = 10_000;
int[,] m1 = Utils.GetMatrix(length);
int[,] m2 = Utils.GetMatrix(length);
var sm1 = new Matrix<int>(m1);
var sm2 = new Matrix<int>(m2);

Matrix<int> resSeq = new Matrix<int>(1);
Matrix<int> resPar = new Matrix<int>(1);

double elapsedSeq = Utils.GetExecutionTakenTime(TimeFormat.Miliseconds,
    () => resSeq = sm1.SequentalAdd(sm2));
double elapsedPar = Utils.GetExecutionTakenTime(TimeFormat.Miliseconds,
    () => resPar = sm1.ParallelAdd(sm2));
double elapsedParFixed = Utils.GetExecutionTakenTime(TimeFormat.Miliseconds,
    () => resPar = sm1.ParallelAdd(sm2, 4));

Console.WriteLine($"Sequental add execution time: {elapsedSeq:F4} ms");
Console.WriteLine($"Parallel add execution time: {elapsedPar:F4} ms");
Console.WriteLine($"Parallel [threads = 4] add execution time: {elapsedParFixed:F4} ms");
Console.WriteLine(resSeq.Length);