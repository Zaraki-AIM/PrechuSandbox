using System;
using System.Collections.Generic;

public class Master
{
    public class Frac :Formula
    {
        public string deno;
        public string enu;

        public Frac(string deno, string enu)
        {
            this.deno = deno;
            this.enu = enu;
        }
    }

    public class Op:Formula
    {
        public string op;

        public Op(string op)
        {
            this.op = op;
        }
    }

    public class Num:Formula
    {
        public string num;

        public Num(string num)
        {
            this.num = num;
        }
    }

    public class Formula
    {
        public string deno;
        public string enu;
    }


    public List<List<Formula>> questionFomulaList = new List<List<Formula>>()
    {
        new List<Formula>{
            new Op(" "),
            new Frac("3","2"),
            new Op("÷"),
            new Frac("6","7")
        },
        new List<Formula>{
            new Op("="),
            new Frac("3","2"),
            new Op("×"),
            new Frac("7","6")
        },
    };

    public List<List<Formula>> bigFractionFomulaList = new List<List<Formula>>()
    {
        new List<Formula>{
            new Num("2"),
            new Op("×"),
            new Num("6")
        },
        new List<Formula>{
            new Num("3"),
            new Op("×"),
            new Num("7")
        },
    };

}
