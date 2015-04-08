using System;

public class mh {
  static readonly Random rand = new Random(); 

  private static double runif(){
    return rand.NextDouble();
  }

  private static double rnorm(double mu, double sd) {
    double u1 = rand.NextDouble();
    double u2 = rand.NextDouble();
    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
    double randNormal = mu + sd * randStdNormal;
    return randNormal;
  }

  private static double dnorm(double x, double mu, double sd) {
    return 1/Math.Sqrt(2*Math.PI*sd*sd) * Math.Exp(Math.Pow((x-mu)/sd,2)/-2);
  }
  
  private static double likelihood(double x, double mu, double sd){
    return Math.Exp(Math.Pow((x-mu)/sd,2)/-2);
  }

  public static void Main() {
    double mu = 3;
    double sd = 2;
    double cs = 5;
    double init = 0;
    int N = 100000;
    double[] draws = new double[N];
    double cand;
    double count = 0;
    draws[0] = init;

    for (int i=1; i<N; i++){
      draws[i] = draws[i-1];
        cand = rnorm(draws[i],cs);
        if ( likelihood(cand,mu,sd) / likelihood(draws[i],mu,sd) > runif() ){
        draws[i] = cand;
        count += 1;
      }
    }
    Console.WriteLine(count/N);

    // Print Data to Text File
    string path = "draws.txt";
    System.IO.File.Delete(path); //Deletes draws.txt if it exists. 
                                 //Does nothing otherwise.
    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path, true)){
      foreach (double draw in draws){
        file.WriteLine(draw);
      }
    }

  }
}
