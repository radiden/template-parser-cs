using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace parser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Wariant> warianty = new();
            warianty.Add(new Wariant
            {
                wariant = "Kolor ramy",
                wartosc = "SDASDAMleko"
            });
            warianty.Add(new Wariant
            {
                wariant = "Kolor szuflady",
                wartosc = "Niebieski"
            });

            List<Cecha> cechy = new();
            cechy.Add(new Cecha
            {
                cecha = "Lóżka, wymiary",
                wartosc = "200 X 90"
            });
            
            var sparsowanySzablon = ParsujSzablon(Vars.ToParse, warianty, cechy);
            
            File.WriteAllText("output.html", sparsowanySzablon);
        }

        static string ParsujSzablon(string szablon, List<Wariant> warianty, List<Cecha> cechy)
        {
            var typeexpr = @"(?i:#\s*(SWITCH|IF)\s*\{\s*(WARIANT|CECHA)\s*\:\s*(.*)\s*\})(?:\s*=\s*(.*))?";
            var switchexpr = @"(?i:@\s*(DEFAULT|VALUE)\s*\:\s*(\w.*)?)";

            List<string> fixedSzablon = PoprawSzablon(szablon);

            List<Match> statements = new();
            foreach (var line in fixedSzablon)
            {
                var match = Regex.Match(line, typeexpr);

                if (match.Success)
                {
                    statements.Add(match);
                }
            }

            List<string> nowySzablon = new();
            foreach (var statement in statements)
            {
                switch (statement.Groups[1].Value.ToLower())
                {
                    case "if":
                        List<string> ifContent = ZnajdzObecnyIf(fixedSzablon, statement);
                        switch (statement.Groups[2].Value.ToLower())
                        {
                            case "cecha":
                                nowySzablon = IfCecha(fixedSzablon, ifContent, statement, cechy);
                                break;
                            case "wariant":
                                nowySzablon = IfWariant(fixedSzablon, ifContent, statement, warianty);
                                break;
                        }
                        break;
                    case "switch":
                        List<string> switchContent = ZnajdzObecnySwitch(fixedSzablon, statement);

                        List<Match> switchvalues = new();
                        foreach (var line in switchContent)
                        {
                            var match = Regex.Match(line, switchexpr);

                            if (match.Success)
                            {
                                switchvalues.Add(match);
                            }
                        }

                        List<string> wartosc = new();
                        switch (statement.Groups[2].Value.ToLower())
                        {
                            case "cecha":
                                wartosc = ZnajdzWartoscDlaCechy(switchvalues, switchContent, statement, cechy);
                                break;
                            case "wariant":
                                wartosc = ZnajdzWartoscDlaWariantu(switchvalues, switchContent, statement, warianty);
                                break;
                        }

                        nowySzablon = ZamienNaWartosc(fixedSzablon, wartosc, switchContent);
                        
                        // List<string> defaultVal = new();
                        // foreach (var value in switchvalues)
                        // {
                        //     switch (value.Groups[1].Value.ToLower())
                        //     {
                        //         case "default":
                        //             defaultVal = ZnajdzTekst(switchContent, switchContent.FindIndex(s => s.StartsWith("@DEFAULT")));
                        //             break;
                        //         case "value":
                        //             switch (statement.Groups[2].Value.ToLower())
                        //             {
                        //                 case "cecha":
                        //                     if (cechy.Any(c => c.cecha.ToLower() == statement.Groups[3].Value.ToLower()))
                        //                     {
                        //                         if (cechy.Any(c => c.wartosc.ToLower() == value.Groups[2].Value.ToLower()))
                        //                         {
                        //                             if (switchContent.Any(s => s == value.Value))
                        //                             {
                        //                                 var wartosc = ZnajdzTekst(switchContent, switchContent.FindIndex(s => s == value.Value));
                        //                             }
                        //                             else
                        //                             {
                        //                                 var wartosc = "";
                        //                             }
                        //                         }
                        //                     }
                        //                     break;
                        //                 case "wariant":
                        //                     break;
                        //             }
                        //             break;
                        //     }
                        // }
                        // switche.Add(switchContent);
                        break;
                }
            }

            StringBuilder parsedSzablon = new();
            
            foreach (var line in nowySzablon)
            {
                parsedSzablon.Append(line + "\n");
            }

            return parsedSzablon.ToString();
        }

        private static List<string> ZnajdzObecnySwitch(List<string> szablon, Match statement)
        {
            List<string> switchContent = new();
            
            bool addLinesUntilEnd = false;
            foreach (var line in szablon)
            {
                if (addLinesUntilEnd)
                {
                    switchContent.Add(line);
                                
                    if (line == "#ENDSWITCH")
                    {
                        break;
                    }
                }
                if (line == statement.Value)
                {
                    switchContent.Add(line);
                    addLinesUntilEnd = true;
                }
            }

            return switchContent;
        }

        private static List<string> ZnajdzObecnyIf(List<string> szablon, Match statement)
        {
            List<string> ifContent = new();

            bool addLinesUntilEnd = false;
            foreach (var line in szablon)
            {
                if (addLinesUntilEnd)
                {
                    ifContent.Add(line);

                    if (line == "}")
                    {
                        break;
                    }
                }

                if (line == statement.Value)
                {
                    ifContent.Add(line);
                    addLinesUntilEnd = true;
                }
            }

            return ifContent;
        }

        private static List<string> PoprawSzablon(string szablon)
        {
            List<string> fixedSzablon = new();
            foreach (var line in szablon.Split("\n"))
            {
                var partiallyFixedLine = line.Replace("\r", "");
                fixedSzablon.Add(Regex.Replace(partiallyFixedLine, " +", " "));
            }

            return fixedSzablon;
        }

        private static List<string> ZnajdzTekst(List<string> switchContent, int index)
        {
            List<string> ret = new();
            bool doIter = true;
            for (var i = index + 1; doIter; i++)
            {
                if (!switchContent[i].StartsWith("@") || !switchContent[i].StartsWith("@"))
                {
                    ret.Add(switchContent[i]);
                }
                else
                {
                    doIter = false;
                }
            }

            return ret;
        }

        private static List<string> ZnajdzWartoscDlaCechy(List<Match> values, List<string> switchContent, Match switchInfo, List<Cecha> cechy)
        {
            var defaultVal = ZnajdzTekst(switchContent, switchContent.FindIndex(s => s.StartsWith("@DEFAULT")));
            var cecha = cechy.FirstOrDefault(c => c.cecha.ToLower() == switchInfo.Groups[3].Value.ToLower());
            if (cecha != null)
            {
                foreach (var value in values.Where(m => m.Groups[1].Value.ToLower() == "value"))
                {
                    if (value.Groups[2].Value.ToLower() == cecha.wartosc.ToLower())
                    {
                        return ZnajdzTekst(switchContent, switchContent.FindIndex(s => s == value.Value));
                    }
                }
                
                return defaultVal;
            }
            
            return defaultVal;
        }

        private static List<string> ZnajdzWartoscDlaWariantu(List<Match> values, List<string> switchContent, Match switchInfo, List<Wariant> warianty)
        {
            var defaultVal = ZnajdzTekst(switchContent, switchContent.FindIndex(s => s.StartsWith("@DEFAULT")));
            var wariant = warianty.FirstOrDefault(w => w.wariant.ToLower() == switchInfo.Groups[3].Value.ToLower());
            if (wariant != null)
            {
                foreach (var value in values.Where(m => m.Groups[1].Value.ToLower() == "value"))
                {
                    if (value.Groups[2].Value.ToLower() == wariant.wartosc.ToLower())
                    {
                        return ZnajdzTekst(switchContent, switchContent.FindIndex(s => s == value.Value));
                    }
                }
                
                return defaultVal;
            }
            
            return defaultVal;
        }

        private static List<string> ZamienNaWartosc(List<string> szablon, List<string> wartosc, List<string> operatorContent)
        {
            var operatorStart = szablon.FindIndex(s => s == operatorContent.First());
            var operatorEnd = (Array.FindIndex(szablon.ToArray()[operatorStart..], s => s == operatorContent.Last())+operatorStart+1);

            szablon.RemoveRange(operatorStart, operatorEnd-operatorStart);
            szablon.InsertRange(operatorStart, wartosc);

            return szablon;
        }

        private static List<string> IfCecha(List<string> szablon, List<string> obecnyIf, Match ifInfo, List<Cecha> cechy)
        {
            if (cechy.Any(c => c.cecha.ToLower() == ifInfo.Groups[3].Value.ToLower()))
            {
                if (cechy.Any(c => c.wartosc.ToLower() == ifInfo.Groups[4].Value.ToLower()))
                {
                    return ZamienIfNaWartosc(szablon, obecnyIf);
                }
            }

            return ZamienNaWartosc(szablon, new List<string>(), obecnyIf);
        }

        private static List<string> IfWariant(List<string> szablon, List<string> obecnyIf, Match ifInfo, List<Wariant> warianty)
        {
            if (warianty.Any(w => w.wariant.ToLower() == ifInfo.Groups[3].Value.ToLower()))
            {
                if (warianty.Any(w => w.wartosc.ToLower() == ifInfo.Groups[4].Value.ToLower()))
                {
                    return ZamienIfNaWartosc(szablon, obecnyIf);
                }
            }

            return ZamienNaWartosc(szablon, new List<string>(), obecnyIf);
        }

        private static List<string> ZamienIfNaWartosc(List<string> szablon, List<string> obecnyIf)
        {
            StringBuilder ifTekst = new();
            foreach (var line in obecnyIf)
            {
                ifTekst.Append(line + "\n");
            }
            var ifExpr = @"#IF.*\n*\{\n([\s\S]*?)(?=\})";
            var match = Regex.Match(ifTekst.ToString(), ifExpr);

            if (match.Success)
            {
                return ZamienNaWartosc(szablon, match.Groups[1].Value.Split("\n").ToList(), obecnyIf);
            }

            return ZamienNaWartosc(szablon, new List<string>(), obecnyIf);
        }
    }
}
