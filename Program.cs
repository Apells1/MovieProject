using System;
using System.Collections; 
using System.IO;
using System.Text;
using NLog.Web;
namespace MovieProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Program started.");
             ArrayList movieNumber = new ArrayList();
              ArrayList movieTitle = new ArrayList();
              ArrayList movieGenre = new ArrayList();
              ArrayList userGenre = new ArrayList();
              try{
                using(StreamReader sr = new StreamReader("Copy of movies.txt")) {
                      sr.ReadLine();
                 while(!sr.EndOfStream) {
                 string reader = sr.ReadLine();
                   double testForComma = reader.IndexOf('"');
                        if (testForComma == -1)
                        {
                            if(reader != null){
                                 string[] entireLine = reader.Split(',');
                                  movieNumber.Add(int.Parse(entireLine[0]));
                      
                           movieTitle.Add(entireLine[1]);
                           
                          var testVar = entireLine[2].Replace("|", ", ");
                           movieGenre.Add(testVar);

                            }
                           
                        }
                        else
                        {
                            
                           if(reader != null){
                                 string[] entireLine = reader.Split('"');
                                  
                                   string removeId = entireLine[0].Replace("," , "");
                                    string removeGenre = entireLine[2].Replace("," , "");

                                     movieNumber.Add(int.Parse(removeId));
                      
                                    movieTitle.Add(entireLine[1]);
                           
                          var testVar = removeGenre.Replace("|", ", ");
                           movieGenre.Add(testVar);

    
                       

                            }
                        }

                 
    }
    
  }
}       
catch(FileNotFoundException){
  Console.WriteLine("Cannot find file");
   logger.Error("File not found");
}          
                        
                       
           
                    

             ArrayList userGenreNP = new ArrayList();
           string userChoice = " ";
            string userChoice2 = " ";
            int idLength = 0;
            var latestId = 0;
            var newId = 0;
            string printName = "";
         Boolean dup = false;
           do{
                Console.WriteLine("Enter 1 to add a new movie to the database.");
            Console.WriteLine("Enter 2 to read from the database.");
            Console.WriteLine("Enter anything else to quit.");

             userChoice = Console.ReadLine();

            if(userChoice == "1"){
            Console.WriteLine("Enter the name of the movie");
            string name = Console.ReadLine();
            printName = name;
            if(name.IndexOf(',') != -1){
               printName = '"'+name+'"';
            }



             Console.WriteLine("Enter the date of the movie");
            string date = Console.ReadLine();
            if(date==""){date = "No date";}
            date = "("+date+")";
            Console.WriteLine(date);
            do {
                
             Console.WriteLine("Enter the genre(s) Enter each genre individually and type '-1' to quit");
           userChoice2 = Console.ReadLine();
           if(userChoice2 != "-1"){
              userGenreNP.Add(userChoice2);
            }
           }
           

            while(userChoice2 != "-1");
            string totalGenreForList = string.Join(", ", (string[])userGenreNP.ToArray(Type.GetType("System.String")));
             string totalGenre = string.Join("|", (string[])userGenreNP.ToArray(Type.GetType("System.String")));
             //its REALLY difficult to join an arraylist together, i got this from stackoverflow but WOW thats a big lone of code!
            //  Console.WriteLine(totalGenre);
              int[] arr = movieNumber.ToArray(typeof(int)) as int[];
              
         idLength = arr.Length;
          latestId = arr[idLength-1];
          newId = latestId+1;
       
        string stringId = newId.ToString();
        string lineToPrint = stringId+", "+printName+" "+date+ ", "+totalGenre;

       
        string[] arrSt = movieTitle.ToArray(typeof(string)) as string[];
        for(var x = 0; x < arrSt.Length; x++){
if(name+" "+date == arrSt[x] || printName+" "+date == arrSt[x]){
dup = true;
}
        }
        if(dup == false){
           movieNumber.Add(newId);
        movieTitle.Add(name);
        movieGenre.Add(totalGenreForList);
         logger.Info("New movie added");
        try{
        using (StreamWriter sw = new StreamWriter("Copy of movies.txt", true, Encoding.Unicode))
            {
                logger.Info("Successfully connected and wrote to file");

                 sw.WriteLine(lineToPrint);
         
            }
       }
       catch(Exception){
         Console.WriteLine("Cannot find file");
         logger.Error("File not found");
       }}

        if(dup == true){
          logger.Info("Duplicate movie was attempted to be added.");
          Console.WriteLine("You entered a movie that already exists, no information was added");
        }
       

            }  

             if(userChoice == "2"){
                 logger.Info("Successfully read from file");
for(var x = 0; x < movieNumber.Count; x++){
        // Console.WriteLine(movieNumber[x]);
        //  Console.WriteLine(movieTitle[x]);
        //   Console.WriteLine(movieGenre[x]);
          Console.WriteLine("{0,-15}{1,12}","Movie Id:", movieNumber[x] );
           Console.WriteLine("{0,-15}{1,12}","Movie Title:", movieTitle[x] );
            Console.WriteLine("{0,-15}{1,12}","Movie Genre:", movieGenre[x] );
            Console.WriteLine();
            
    }
                
            }

               

           }
           while(userChoice == "1" || userChoice == "2");
            logger.Info("program ended.");
        }
    }
}
