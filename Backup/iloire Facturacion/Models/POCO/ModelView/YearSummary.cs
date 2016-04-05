using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class YearSummary
{
    public Summary Q1 { get; set; }
    public Summary Q2 { get; set; }
    public Summary Q3 { get; set; }
    public Summary Q4 { get; set; }
}