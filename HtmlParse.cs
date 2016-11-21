using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using CsQuery;

namespace TribalWarsBot {

    public class HtmlParse {

        public static int GetCurrentLevelOfBuilingFromTableRow(CQ element) {
            if (element.Text().Length == 0)
                return 0; //the bulding has not been build yet

            if (!element.Is("tr"))
                throw new Exception("Did expect a table row (tr)");

            var levelText = element.Text();

            var numberRegEx = new Regex(@"Nivå (\d{1,2})");


            var match = numberRegEx.Match(levelText);


            if(!match.Success)
                throw new Exception("Did not find a level");

            return int.Parse(match.Groups[1].Value);
        }

        public static IDomElement GetElementFromHtmlTable(List<IDomObject> table, int row, int col)
        {
            var chidlEle = table[row].ChildElements;
            var list = chidlEle.ToList();
            var ele = list[col];
            return ele;
        }

    }

}