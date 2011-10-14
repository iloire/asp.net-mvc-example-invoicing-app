using System;

public class TaxDateHelper {


    public static DateTime GetEndDate(int q, int year)
    {
        if (q == 1)
            return GetStartDate(2,year).AddDays(-1);
        else if (q == 2)
            return GetStartDate(3, year).AddDays(-1);
        else if (q == 3)
            return GetStartDate(4, year).AddDays(-1);
        else if (q == 4)
            return new DateTime(year, 12, 31);
        else
            throw new ArgumentException("Invalid quarter (valid numbers from 1 to 4)");
    } 


    public static DateTime GetStartDate(int q, int year){
        if (q == 1)
            return new DateTime(year, 1, 1);
        else if (q == 2)
            return new DateTime(year, 4, 1);
        else if (q == 3)
            return new DateTime(year, 7, 1);
        else if (q == 4)
            return new DateTime(year, 10, 1);
        else
            throw new ArgumentException("Invalid quarter (valid numbers from 1 to 4)");

    } 
    
       
    public  static void CalculateQuarter(DateTime date, out int quarter, out int year, out DateTime start, out DateTime end)
    {
        year=date.Year;
        if (date < GetStartDate(1,year)){
            quarter = 1;

            start = GetStartDate(1,year);
            end = GetStartDate(2,year).AddDays(-1);
        }
        else if (date < GetStartDate(3,year))
        {
            quarter = 2;

            start = GetStartDate(2,year);
            end = GetStartDate(3,year).AddDays(-1);
        }
        else if (date < GetStartDate(4,year))
        {
            quarter = 3;

            start = GetStartDate(3,year);
            end = GetStartDate(4,year).AddDays(-1);
        }
        else
        {
            quarter = 4;

            start = GetStartDate(4,year);
            end = new DateTime(year, 12, 31);
        }
      
    }

}