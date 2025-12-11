using System;


class StackVsHeap
{
    static void Main(string[] args)
    {
        /* value types such as integer are stored on the stack 
         * Object/classes Types are store on heap 
         * pointer to them are stored on the stack */
        int score = 95; //stack
        int score2 = score; //stack
        Console.WriteLine($"Score = {score}, Score2 = {score2}");

        /*when you create an object it will belong to an heap
         but the pointer is belong to stack which is (report)
         and the values on object is belong to an heap*/
        Report report = new Report()
        {
            Title = "First Report",
            Desc = "This is the first report",
            Pages = 11
        };

        //we copy an pointer so same result
        Report report2 = report;
        Console.WriteLine(report.Display());
        Console.WriteLine(report2.Display());

        //but we change the value here so both value of pointer are same
        //because we copy the previous pointer
        report2.Pages = 999;
        Console.WriteLine(report.Display());
        Console.WriteLine(report2.Display());
        //but if the objects are struct we can still copy the value
        //but if we change a value the other object or pointer it wont change
    }

    class Report
    {
        public String Title { get; set; }
        public String Desc { get; set; }
        public int Pages { get; set; }

        public String Display()
        {
            return String.Format("{0}: {1}, pp. {2}", Title, Desc, Pages);
        }
    }
}
