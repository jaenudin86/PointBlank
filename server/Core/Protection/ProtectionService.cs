using System;
using System.Collections.Generic;
using Core.Model.Protection;
using System.Threading;

namespace Core.Protection
{
    public class ProtectionService
    {
        public static Dictionary<string, Attack> attackers;

        public static void Initialization()
        {
            try
            {
                attackers = new Dictionary<string, Attack>();
                var check = new Thread(CheckList);
                check.Start();
                Logger.Protection("[Protection] Service Started.");
            }
            catch (Exception ex)
            {
                Logger.Error("[Protection] Error {0}", ex);
            }
        }

        public static void CheckList()
        {
            for (;;)
            {
                foreach (var attack in attackers)
                {
                    if (DateTime.Now.CompareTo(attackers[attack.Key].connectDate) < 0 & DateTime.Now.CompareTo(attackers[attack.Key].banDate) < 0)
                    {
                        attackers[attack.Key].Status = false;
                        Logger.Protection("[Protection] Remove " + attack.Value.IPv4 + " to suspects list.");
                        attackers[attack.Key].connectDate = DateTime.Now.AddMinutes(1);
                        attackers[attack.Key].Connect = false;
                        attackers[attack.Key].count = 0;
                    }
                }
                Thread.Sleep(200);
            }
        }

        public static void addAttack(string IPv4)
        {
            try
            {
                if(attackers.ContainsKey(IPv4))
                {
                    if(attackers[IPv4].Status == false)
                    {
                        if (attackers[IPv4].count < 3)
                        {
                            attackers[IPv4].count = attackers[IPv4].count + 1;
                        }
                        else
                        {
                            attackers[IPv4].banDate = DateTime.Now.AddMinutes(5);
                            attackers[IPv4].Status = true;
                            Logger.Protection("[Protection] Banned " + IPv4 + " to " + DateTime.Now.AddMinutes(1));
                        }
                    }
                }
                else
                {
                    Attack attack = new Attack()
                    {
                        IPv4 = IPv4,
                        Connect = false,
                        connectDate = DateTime.Now.AddMinutes(1),
                        Status = false,
                        count = 1
                    };
                    attackers.Add(attack.IPv4, attack);
                    Logger.Protection("[Protection] Add " + IPv4 + " to suspects list.");
                }
            }
            catch(Exception e)
            {
                Logger.Error("[Protection] Error {0}", e);
            }
        }
    }
}