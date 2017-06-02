using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMax.MarketingTech.RulesEngine
{
    class caseProcessor
    {
        #region applyCaseRules
         public static bool caseProc(ConcurrentDictionary<string, string> row, int ruleno, int maxCase, RulesXmlReader x, LogicProcessing logicob, List<string[]> totalLists)
        {
            int currentCaseno = 1; bool endRule=false;

            while (currentCaseno <= maxCase)
            {
                var caseCond = x.RulesCaseConditions().FirstOrDefault(xx => xx.Key == currentCaseno).Value;
                var caseStat = x.RulesCaseStatements().FirstOrDefault(xxy => xxy.Key == currentCaseno).Value;
                if (caseCond == null || caseCond.Count == 0)
                {
                    break;
                }
                if (logicob.ProcessConditions(ruleno, caseCond.FirstOrDefault().ToString(), inputParser.tableAttributess[ruleno].ToString(), row, totalLists))
                {
                    foreach (var item in caseStat)
                    {
                        var stm=logicob.ProcessStatements(currentCaseno, item, inputParser.tableAttributess[ruleno].ToString(), row, totalLists);
                       if(stm)
                       {
                           return true;
                       }
                    }
                }
                currentCaseno++;
            }
            return endRule;

        }
       #endregion applyCaseRules
    }
}
