using System.Dynamic;
using System.Xml;

namespace SD
{
    internal class Program
    {
        class Human
        {
            public string name;
            public int lv;
            public int[] stats;
        }

        class Hero : Human
        {
            public string job;
            public int jobCode;
            public int gold;
            public int exp;
            public int[] maxExp;
            public int[] gearStats;
            public int[,] equippedGear;
            public List<string[]> inventoryList = new List<string[]>();
            public Hero()
            {
                stats = [10, 5, 100];
                gold = 3500;
                lv = 1;
                gearStats = [0, 0, 0];
                equippedGear = new int[,] { { 0, 0 }, { 0, 0 } };
                exp = 0;
                maxExp = [0, 10, 30, 150, 300, 1000];
            }
        }
        class Gear
        {
            public string[] name;
            public string[] description; 
            public string[] value;
            public string[] equipped;
            public string[] sellValue;
        }
        class Sword : Gear
        {
            public int[] atk;
            public int[] type;
        }
        class Armor : Gear
        {
            public int[] def;
            public int[] hp;
            public int[] type;
        }
        static void Main(string[] args)
        {
            Sword sword = new Sword();
            Armor armor = new Armor();
            Hero hero = new Hero();
            int getInt = 0;
            string getString;
            LoadGear(sword, armor);

        
            Console.Clear();
        FirstMain:
            Console.WriteLine("\n환영합니다.");
            Console.WriteLine("\n이름을 입력해주세요\n");
            hero.name = (Console.ReadLine());


            Console.Clear();
            Console.WriteLine("\n직업을 선택해 주세요");
            Console.WriteLine("\n1.전사\n2.도적\n");

            Console.WriteLine("\n번호를 입력해 주세요");
            Console.Write(">>");
            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                hero.job = "전사";
                hero.jobCode = 0;
            }
            else if (getInt == 2)
            {
                hero.job = "도적";
                hero.jobCode = 1;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함\n");
                goto FirstMain;
            }

            Console.Clear();
        SecondMain:
            Console.WriteLine("\n1.스텟\n2.인벤\n3.상점\n4.던전 입장\n5.휴식");
            Console.WriteLine("\n행동을 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                ShowStats(hero);
                Console.Clear();
                goto SecondMain;
            }
            else if (getInt == 2)
            {
                ShowInventory(hero);
                Console.Clear();
                goto SecondMain;
            }
            else if (getInt == 3)
            {
                ShowStore(sword, armor, hero);
                Console.Clear();
                goto SecondMain;
            }
            else if (getInt == 4)
            {
                int result;
                Console.Clear();
                Console.WriteLine("\n던전 Lv1\t방어력 5이상 권장\n");
                Console.WriteLine("던전 Lv2\t방어력 11이상 권장\n");
                Console.WriteLine("던전 Lv3\t방어력 17이상 권장\n");
                Console.WriteLine("0.나가기");
                Console.WriteLine("\n번호를 입력해 주세요");
                Console.Write(">>");
                getInt = Input(Console.ReadLine());

                if (getInt == 1)
                {
                    GoToDungeon(5, hero.stats[1], hero);
                    Console.Clear();
                    goto SecondMain;
                }
                else if (getInt == 2)
                {
                    GoToDungeon(11, hero.stats[1], hero);
                    Console.Clear();
                    goto SecondMain;
                }
                else if (getInt == 3)
                {
                    GoToDungeon(17, hero.stats[1], hero);
                    Console.Clear();
                    goto SecondMain;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력");
                    goto SecondMain;
                }
            }
            else if (getInt == 5)
            {
                Console.Clear();
                Console.WriteLine("\n500골드를 내고 휴식을 취하시겠습니까?\n");
                Console.WriteLine("현재 보유 골드 : {0}\n", hero.gold);
                Console.WriteLine("1.예  2.아니요\n");
                Console.Write(">>");
                getInt = Input(Console.ReadLine());

                if (getInt == 1)
                {
                    GetSomeRest(hero);
                }
                else if (getInt == 2)
                {
                    Console.Clear();
                    goto SecondMain;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력");
                    goto SecondMain;
                }
                Console.Clear();
                Console.WriteLine("잘못된 값 입력");
                goto SecondMain;
            }
            else
            {
                Console.WriteLine("잘못된 값 입력함");
                goto SecondMain;
            }
        }

        static void LoadGear(Sword sword, Armor armor)
        {
            sword.name = ["낡은 검", "청동 도끼", "스파르타의 창"];
            sword.description = ["쉽게 볼 수 있는 낡은 검\t", "어디선가 사용됐던거 같은 도끼", "스파르타에서 사용한 전설의 창"];
            sword.value = ["600", "1500", "3000"];
            sword.sellValue = ["300", "750", "1500"];
            sword.atk = [2, 5, 7];
            sword.equipped = ["N", "N", "N"];
            sword.type = [1, 1, 1];

            armor.name = ["수련자 갑옷", "무쇠 갑옷", "스파르타 갑옷"];
            armor.description = ["수련에 도움을 주는 갑옷\t", "무쇠로 만들어져 튼튼한 갑옷\t", "스파르타에서 사용한 전설의 갑옷"];
            armor.value = ["1000", "2000", "3500"];
            armor.sellValue = ["500", "1000", "1750"];
            armor.def = [5, 9, 15];
            armor.equipped = ["N", "N", "N"];
            armor.type = [2, 2, 2];
        }

        static void ShowStats(Hero hero)
        {
            int getInt = 0;
            string getString;
            int maxExp = hero.maxExp[hero.lv];
            int exp = hero.exp;
            int expSquare = exp * 10 / maxExp;
            Console.Clear();
        StatsAgain:
            Console.WriteLine("\n\t[스텟]\t\n");
            Console.WriteLine("Lv. {0}", hero.lv);
            Console.WriteLine("{0} ({1})", hero.name, hero.job);

            if (hero.gearStats[0] > 0)
            {
                Console.WriteLine("공격력 : {0} (+{1})", hero.stats[0] + hero.gearStats[0], hero.gearStats[0]);
            }
            else
            {
                Console.WriteLine("공격력 : {0}", hero.stats[0]);
            }

            if (hero.gearStats[1] > 0)
            {
                Console.WriteLine("방어력 : {0} (+{1})", hero.stats[1] + hero.gearStats[1], hero.gearStats[1]);
            }
            else
            {
                Console.WriteLine("방어력 : {0}", hero.stats[1]);
            }

            if (hero.gearStats[2] > 0)
            {
                Console.WriteLine("체 력 : {0} (+{1})", hero.stats[2] + hero.gearStats[2], hero.gearStats[2]);
            }
            else
            {
                Console.WriteLine("체 력 : {0}", hero.stats[2]);
            }

            Console.WriteLine("Gold : {0} G", hero.gold);
            Console.WriteLine("\nExp : {0}/{1}", exp, maxExp);

            for (int i = 0; i < expSquare; i++)
            {
                Console.Write("■");
            }
            for (int i = 0; i < 10 - expSquare; i++)
            {
                Console.Write("□");
            }
            Console.WriteLine("\n");


            Console.WriteLine("\n0.나가기");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt == 0)
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto StatsAgain;
            }
        }

        static void ShowInventory(Hero hero)
        {
            int getInt = 0;
            string getString;
            int invListCount = hero.inventoryList.Count;
            Console.Clear();
        InventoryMain:
            Console.WriteLine("\n\t[인벤토리]\t\n");

            PrintInvenList(hero, invListCount, 1);

            Console.WriteLine("\n\t\t\t\t\t골드 : {0}G", hero.gold);

            Console.WriteLine("\n1.장착관리  0.나가기");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");
            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                InventoryManagement(hero);
                Console.Clear();
                goto InventoryMain;
            }
            else if (getInt == 0)
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto InventoryMain;
            }
        }

        static void InventoryManagement(Hero hero)
        {
            int getInt = 0;
            string getString;
            int invListCount = hero.inventoryList.Count;
            Console.Clear();
        InvManMain:
            Console.WriteLine("\n\t[인벤토리 장착관리]\t\n");

            PrintInvenList(hero, invListCount, 2);

            Console.WriteLine("\n장착할 장비의 번호를 선택해주세요  0.나가기");
            Console.WriteLine("번호를 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt > 0 && getInt <= invListCount)
            {
                InventoryEquip(hero, getInt);
                Console.Clear();
                goto InvManMain;
            }
            else if (getInt == 0)
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 갑 입력함");
                goto InvManMain;
            }
        }

        static void ShowStore(Sword sword, Armor armor, Hero hero)
        {
            int getInt = 0;
            int iNum = 0;
            int swordCount = sword.name.Length;
            int armorCount = armor.name.Length;
            string getString;
            string[] sArr = new string[7];  
            
            Console.Clear();
        StoreAgain:
            Console.WriteLine("\n\t[상점]\t\n");

            PrintStoreItem(sword, armor, swordCount, armorCount, 1);
            Console.WriteLine("\n\t\t\t골드 : {0}G", hero.gold);

            Console.WriteLine("\n1.아이템 구매  2.아이템 판매  3.인벤토리  0.나가기");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                //아이템 구매 창
                int[] iNumArr = StorePurchase(sword, armor);

                if (iNumArr[0] == 0)
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 갑 입력함");
                    goto StoreAgain;
                }

                if (iNumArr[1] == 1)
                {
                    if (hero.gold >= int.Parse(sword.value[iNumArr[0] - 1]))
                    {
                        hero.gold -= int.Parse(sword.value[iNumArr[0] - 1]);
                        sArr = [sword.name[iNumArr[0] - 1], sword.atk[iNumArr[0] - 1].ToString(), sword.description[iNumArr[0] - 1], sword.value[iNumArr[0] - 1], sword.equipped[iNumArr[0] - 1], sword.type[iNumArr[0] - 1].ToString(), sword.sellValue[iNumArr[0] - 1]];
                        hero.inventoryList.Add(sArr);
                        sword.value[iNumArr[0] - 1] = "구매한 상품입니다.";

                        Console.Clear();
                    PurchaseComplete1:
                        Console.WriteLine("구매가 완료되었습니다.  0.나가기");
                        Console.WriteLine("\n번호를 입력해주세요");
                        Console.Write(">>");
                        getInt = Input(Console.ReadLine());

                        if (getInt == 0)
                        {
                            Console.Clear();
                            goto StoreAgain;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("잘못된 값 입력");
                            goto PurchaseComplete1;
                        }
                    }
                    else
                    {
                    NotEnoughGold1:
                        Console.WriteLine("골드가 부족합니다.  0.나가기");
                        Console.WriteLine("\n번호를 입력해주세요");
                        Console.Write(">>");
                        getInt = Input(Console.ReadLine());

                        if (getInt == 0)
                        {
                            Console.Clear();
                            goto StoreAgain;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("잘못된 값 입력");
                            goto NotEnoughGold1;
                        }
                    }

                }
                else if (iNumArr[1] == 2)
                {
                    if (hero.gold >= int.Parse(armor.value[iNumArr[0] - 1]))
                    {
                        hero.gold -= int.Parse(armor.value[iNumArr[0] - 1]);
                        sArr = [armor.name[iNumArr[0] - 1], armor.def[iNumArr[0] - 1].ToString(), armor.description[iNumArr[0] - 1], armor.value[iNumArr[0] - 1], armor.equipped[iNumArr[0] - 1], armor.type[iNumArr[0] - 1].ToString(), armor.sellValue[iNumArr[0] - 1]];
                        hero.inventoryList.Add(sArr);
                        armor.value[iNumArr[0] - 1] = "구매한 상품입니다.";

                        Console.Clear();
                    PurchaseComplete2:
                        Console.WriteLine("\n구매가 완료되었습니다.  0.나가기");
                        Console.WriteLine("\n번호를 입력해주세요");
                        Console.Write(">>");
                        getInt = Input(Console.ReadLine());

                        if (getInt == 0)
                        {
                            Console.Clear();                            
                            goto StoreAgain;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("잘못된 값 입력");
                            goto PurchaseComplete2;
                        }
                    }
                    else
                    {
                    NotEnoughGold2:
                        Console.WriteLine("골드가 부족합니다.  0.나가기");
                        Console.WriteLine("\n번호를 입력해주세요");
                        Console.Write(">>");
                        getInt = Input(Console.ReadLine());

                        if (getInt == 0)
                        {
                            Console.Clear();
                            goto StoreAgain;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("잘못된 값 입력");
                            goto NotEnoughGold2;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (getInt == 2)
            {
                SellGear(hero);
                goto StoreAgain;
            }
            else if (getInt == 3)
            {
                ShowInventory(hero);
            }
            else if (getInt == 0)
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto StoreAgain;
            }
            Console.Clear();
            goto StoreAgain;
        }

        static int[] StorePurchase(Sword sword, Armor armor)
        {
            int getInt = 0;
            int swordCount = sword.name.Length;
            int armorCount = armor.name.Length;
            int iReturn;
            int[] iReturnArr;
            string getString;
        
            Console.Clear();
        PurchaseAgain:
            Console.WriteLine("\t[상점]\t\n");

            PrintStoreItem(sword, armor, swordCount, armorCount, 2);

            Console.WriteLine("\n구매할 아이템 번호 선택  0.나가기");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt > 0 && getInt <= swordCount)
            {
                if (sword.value[getInt - 1] == "구매한 상품입니다.")
                {
                    Console.Clear();
                AleadyPurchased:
                    Console.WriteLine("\n이미 구매한 상품입니다.");
                    Console.WriteLine("\n0.나가기");
                    Console.WriteLine("\n번호를 입력해주세요");
                    Console.Write(">>");

                    getInt = Input(Console.ReadLine());

                    if (getInt == 0)
                    {
                        Console.Clear();
                        goto PurchaseAgain;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 값 입력함");
                        goto AleadyPurchased;
                    }
                }
                iReturn = PurchaseGear(getInt, sword);
                iReturnArr = [iReturn, 1];
                return iReturnArr;
            }
            else if (getInt > swordCount && getInt <= swordCount + armorCount)
            {
                if (armor.value[getInt - swordCount - 1] == "구매한 상품입니다.")
                {
                    Console.Clear();
                AleadyPurchased:
                    Console.WriteLine("\n이미 구매한 상품입니다.");
                    Console.WriteLine("\n0.나가기");
                    Console.WriteLine("\n번호를 입력해주세요");
                    Console.Write(">>");

                    getInt = Input(Console.ReadLine());

                    if (getInt == 0)
                    {
                        Console.Clear();
                        goto PurchaseAgain;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 값 입력함");
                        goto AleadyPurchased;
                    }
                }
                iReturn = PurchaseGear(getInt - swordCount, armor);
                iReturnArr = [iReturn, 2];
                return iReturnArr;
            }
            else if (getInt == 0)
            {
                return [0, 0];
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto PurchaseAgain;
            }

        }
        static int PurchaseGear(int iNum, Sword sword)
        {
            int getInt = 0;
            string getString;       
            Console.Clear();
        PurchaseAgain:
            Console.WriteLine("\t[구매]\t\n");

            Console.WriteLine("- {0}\t | {1}\t | {2}\t | {3}", sword.name[iNum - 1], sword.atk[iNum - 1], sword.description[iNum - 1], sword.value[iNum - 1]);


            Console.WriteLine("\n구매하시겠습니까?\n1.예  2.아니요");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                //구매
                return iNum;
            }
            else if (getInt == 2)
            {
                return 0; ;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto PurchaseAgain;
            }
        }
        static int PurchaseGear(int iNum, Armor armor)
        {
            int getInt = 0;
            string getString;
        
            Console.Clear();
        PurchaseAgain:
            Console.WriteLine("\t[구매]\t\n");

            Console.WriteLine("- {0}\t | {1}\t | {2}\t | {3}", armor.name[iNum - 1], armor.def[iNum - 1], armor.description[iNum - 1], armor.value[iNum - 1]);


            Console.WriteLine("\n구매하시겠습니까?\n1.예  2.아니요");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                //구매
                return iNum;
            }
            else if (getInt == 2)
            {
                return 0;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto PurchaseAgain;
            }
        }

        static void SellGear(Hero hero)
        {      
            int getInt2 = 0;
            string getString2;
            int invListCount = hero.inventoryList.Count;
            Console.Clear();
        SellInventoryMain:
            Console.WriteLine("\t[상점 판매]\t\n");

            PrintInvenList(hero, invListCount, 2);

            Console.WriteLine("\n판매할 물건의 번호를 입력해주세요  0.나가기");
            Console.WriteLine("\n번호를 입력해주세요");
            Console.Write(">>");

            getInt2 = Input(Console.ReadLine());

            if (getInt2 > 0 && getInt2 <= hero.inventoryList.Count)
            {
                if (hero.inventoryList[getInt2 - 1][4] == "E")
                {
                    if (hero.inventoryList[getInt2 - 1][5] == "1")
                    {
                        hero.gearStats[0] -= int.Parse(hero.inventoryList[getInt2 - 1][1]);
                    }
                    else if (hero.inventoryList[getInt2 - 1][5] == "2")
                    {
                        hero.gearStats[1] -= int.Parse(hero.inventoryList[getInt2 - 1][1]);
                    }
                }

                hero.gold += int.Parse(hero.inventoryList[getInt2 - 1][6]);
                hero.inventoryList.Remove(hero.inventoryList[getInt2 - 1]);

                Console.Clear();
            PurchaseComplete:
                Console.WriteLine("\n판매가 완료되었습니다.  0.나가기");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                getInt2 = Input(Console.ReadLine());

                if (getInt2 == 0)
                {
                    goto SellInventoryMain;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto PurchaseComplete;
                }
            }
            else if (getInt2 == 0)
            {
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 값 입력함");
                goto SellInventoryMain;
            }
        }

        static void InventoryEquip(Hero hero, int getInt)
        {
            int getInt2;
            string getString2;
            Console.Clear();
        EquipMain:
            Console.WriteLine("\t[인벤토리 장착관리]\t\n");

            Console.WriteLine("- {0}\t   | {1}\t | {2}", hero.inventoryList[getInt - 1][0], hero.inventoryList[getInt - 1][1], hero.inventoryList[getInt - 1][2]);

            if (hero.inventoryList[getInt - 1][4] == "N")
            {
                Console.WriteLine("\n1.장착  0.나가기");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                getInt2 = Input(Console.ReadLine());

                if (getInt2 == 1)
                {
                    hero.inventoryList[getInt - 1][4] = "E";
                    if (hero.inventoryList[getInt - 1][5] == "1")
                    {
                        if (hero.equippedGear[0, 0] == 1)
                        {
                            unequipGear(hero.equippedGear[0, 1], hero);
                        }
                        hero.gearStats[0] += int.Parse(hero.inventoryList[getInt - 1][1]);
                        hero.equippedGear[0, 0] = 1;
                        hero.equippedGear[0, 1] = getInt;
                    }
                    else if (hero.inventoryList[getInt - 1][5] == "2")
                    {
                        if (hero.equippedGear[1, 0] == 1)
                        {
                            unequipGear(hero.equippedGear[1, 1], hero);
                        }
                        hero.gearStats[1] += int.Parse(hero.inventoryList[getInt - 1][1]);
                        hero.equippedGear[1, 0] = 1;
                        hero.equippedGear[1, 1] = getInt;
                    }
                }
                else if (getInt2 == 0)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto EquipMain;
                }
            }
            else
            {
                Console.WriteLine("\n1.해제  0.나가기");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                getInt2 = Input(Console.ReadLine());

                if (getInt2 == 1)
                {
                    unequipGear(getInt, hero);
                }
                else if (getInt2 == 0)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto EquipMain;
                }
            }
        }

        static void unequipGear(int getInt, Hero hero)
        {
            hero.inventoryList[getInt - 1][4] = "N";
            if (hero.inventoryList[getInt - 1][5] == "1")
            {
                hero.gearStats[0] -= int.Parse(hero.inventoryList[getInt - 1][1]);
                hero.equippedGear[0, 0] = 0;
                hero.equippedGear[0, 1] = 0;
            }
            else if (hero.inventoryList[getInt - 1][5] == "2")
            {
                hero.gearStats[1] -= int.Parse(hero.inventoryList[getInt - 1][1]);
                hero.equippedGear[1, 0] = 0;
                hero.equippedGear[1, 1] = 0;
            }
        }
        static void PrintInvenList(Hero hero, int invListCount, int printType)
        {
            Console.WriteLine("\t이름\t\t능력치\t\t설명");
            Console.WriteLine("----------------------------------------------------------------");
            if (printType == 1)
            {
                for (int i = 0; i < invListCount; i++)
                {
                    if (hero.inventoryList[i][4] == "N")
                    {
                        if (hero.inventoryList[i][5] == "1")
                        {
                            Console.WriteLine("- {0}\t   | 공격력 +{1}\t  | {2}\n", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                        else
                        {
                            Console.WriteLine("- {0}\t   | 방어력 +{1}\t  | {2}\n", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }

                    }
                    else if (hero.inventoryList[i][4] == "E")
                    {
                        if (hero.inventoryList[i][5] == "1")
                        {
                            Console.WriteLine("- [E]{0}\t   | 공격력 +{1}\t  | {2}\n", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                        else
                        {
                            Console.WriteLine("- [E]{0}\t   | 방어력 +{1}\t  | {2}\n", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                    }
                }
                Console.WriteLine("----------------------------------------------------------------");
            }
            else if (printType == 2)
            {
                for (int i = 0; i < invListCount; i++)
                {
                    if (hero.inventoryList[i][4] == "N")
                    {
                        if (hero.inventoryList[i][5] == "1")
                        {
                            Console.WriteLine("-{0} {1}\t   | 공격력 +{2}\t  | {3}\n", i + 1, hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                        else
                        {
                            Console.WriteLine("-{0} {1}\t   | 방어력 +{2}\t  | {3}\n", i + 1, hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }    
                    }
                    else if (hero.inventoryList[i][4] == "E")
                    {
                        if (hero.inventoryList[i][5] == "1")
                        {
                            Console.WriteLine("-{0} [E]{1}\t   | 공격력 +{2}\t  | {3}\n", i + 1, hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                        else
                        {
                            Console.WriteLine("-{0} [E]{1}\t   | 방어력 +{2}\t  | {3}\n", i + 1, hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                    }
                }
            }
        }
        static void PrintStoreItem(Sword sword, Armor armor, int swordCount, int armorCount, int printType)
        {
            Console.WriteLine("\t이름\t\t능력치\t\t설명\t\t\t\t    가격");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            if (printType == 1)
            {
                for (int i = 0; i < swordCount; i++)
                {
                    Console.WriteLine("- {0}\t   | 공격력 +{1}\t  | {2}\t  | {3}G\n", sword.name[i], sword.atk[i], sword.description[i], sword.value[i]);
                }
                for (int i = 0; i < armorCount; i++)
                {
                    Console.WriteLine("- {0}\t   | 방어력 +{1}\t  | {2}\t  | {3}G\n", armor.name[i], armor.def[i], armor.description[i], armor.value[i]);
                }
            }
            else if (printType == 2)
            {
                for (int i = 0; i < swordCount; i++)
                {
                    Console.WriteLine("{0} {1}\t | 공격력 +{2}\t  | {3}\t  | {4}G\n", i + 1, sword.name[i], sword.atk[i], sword.description[i], sword.value[i]);
                }
                for (int i = 0; i < armorCount; i++)
                {
                    Console.WriteLine("{0} {1}\t | 방어력 +{2}\t  | {3}\t  | {4}G\n", i + 1 + swordCount, armor.name[i], armor.def[i], armor.description[i], armor.value[i]);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------\n");
        }
        static void GetSomeRest(Hero hero)
        {
            int getInt;
            if(hero.gold >= 500)
            {
                hero.stats[2] = 100;
                hero.gold -= 500;
                Console.Clear();
            RestMain1:
                Console.WriteLine("\n휴식을 충분히 취했습니다.\n");
                Console.WriteLine("체력이 100으로 회복되었습니다.");

                Console.WriteLine("0.나가기");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                getInt = Input(Console.ReadLine());
                if (getInt == 0)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto RestMain1;
                } 
            }
            else
            {
                Console.Clear();
            RestMain2:
                Console.WriteLine("\n골드가 부족합니다.\n");
                Console.WriteLine("0.나가기");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                getInt = Input(Console.ReadLine());
                if (getInt == 0)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto RestMain2;
                }
            }
        }
        static int Input(string getString)
        {
            int getInt;
            try
            {
                getInt = int.Parse(getString);
                return getInt;
            }
            catch (FormatException)
            {
                Console.WriteLine("잘못된 값 입력함");
                return 0;
            }
        }
        static void GoToDungeon(int dunLevel, int myDefence, Hero hero)
        {
            Random rand = new Random();
            int iValue = 0, result = 0, iNum = 0;
            int goldGain = 0;
            int damage = dunLevel - myDefence;
            int extraDmg = 0;
            int[] iLevUP = [0, 0];
            if (myDefence > dunLevel)
            {
                iValue = rand.Next(20, 36);
                hero.stats[2] -= iValue + damage;
                iLevUP = GetExp(hero, dunLevel);

                if (hero.stats[2] <= 0)
                {
                    HeroDie(hero);
                }

                result = 1;
            }
            else if (myDefence <= dunLevel)
            {
                iValue = rand.Next(0, 10);
                if (iValue < 4)
                {
                    //던전공략실패
                    extraDmg = hero.stats[2] / 2;
                    hero.stats[2] -= iValue + damage + extraDmg;

                    if (hero.stats[2] <= 0)
                    {
                        HeroDie(hero);
                    }

                    result = 0;
                }
                else
                {
                    iValue = rand.Next(20, 36);
                    hero.stats[2] -= iValue + damage;
                    iLevUP = GetExp(hero, dunLevel);

                    if (hero.stats[2] <= 0)
                    {
                        HeroDie(hero);
                    }

                    result = 1;
                }
            }

            if (result == 1)
            {
                switch (dunLevel)
                {
                    case 5:
                        goldGain = 1000 + 1000 * rand.Next(10, 21) / 100;
                        break;
                    case 11:
                        goldGain = 1100 + 1100 * rand.Next(10, 21) / 100;
                        break;
                    case 17:
                        goldGain = 1700 + 1700 * rand.Next(10, 21) / 100;
                        break;
                }
                hero.gold += goldGain;

                Console.Clear();
            DunClearMain:
                Console.WriteLine("\n던전을 클리어 하였습니다.\n");

                Console.WriteLine("체력 : {0} -> {1}", hero.stats[2] + iValue + damage, hero.stats[2]);
                Console.WriteLine("Gold : {0} -> {1}", hero.gold - goldGain, hero.gold);
                Console.WriteLine("경험치 {0}획득!", iLevUP[1]);
                if (iLevUP[0] > 0)
                {
                    Console.WriteLine("\n레벨업!");
                    Console.WriteLine("Lv {0} -> {1}\tExp {2}/{3}", hero.lv - iLevUP[0], hero.lv, hero.exp, hero.maxExp[hero.lv]);
                }
                else
                {
                    Console.WriteLine("Lv {0}\tExp {1}/{2}", hero.lv, hero.exp, hero.maxExp[hero.lv]);
                }

                    Console.WriteLine("\n0.확인");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                iNum = Input(Console.ReadLine());

                if (iNum == 0)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto DunClearMain;
                }
            }
            else
            {
                Console.Clear();
            DunFailMain:
                Console.WriteLine("\n던전을 클리어 하지 못했습니다.\n");

                Console.WriteLine("체력 : {0} -> {1}", hero.stats[2] + iValue + damage + extraDmg, hero.stats[2]);
                Console.WriteLine("Gold : {0} -> {1}", hero.gold - goldGain, hero.gold);

                Console.WriteLine("\n0.확인\n");
                Console.WriteLine("\n번호를 입력해주세요");
                Console.Write(">>");

                iNum = Input(Console.ReadLine());

                if (iNum == 0)
                {
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 값 입력함");
                    goto DunFailMain;
                }
            }

        }
        static int[] GetExp(Hero hero, int dungeon)
        {
            int getExp, a;
            int[] iArr = new int[2];
            Random rand = new Random();

            switch(dungeon)
            {
                case 5:
                    getExp = rand.Next(4, 8);
                    break;
                case 11:
                    getExp = rand.Next(15, 31);
                    break;
                case 17:
                    getExp = rand.Next(100, 151);
                    break;
                default:
                    getExp = rand.Next(4, 8);
                    break;
            }
            a = CaculateExp(hero, getExp, 0);
            iArr[0] = a;
            iArr[1] = getExp;
            return iArr;
        }
        static int CaculateExp(Hero hero, int getExp, int i)
        {
            int count = i;
            if(hero.exp + getExp < hero.maxExp[hero.lv])
            {
                hero.exp += getExp;
                return count;
            }
            else if (hero.exp + getExp >= hero.maxExp[hero.lv])
            {
                int j = hero.exp - hero.maxExp[hero.lv];
                hero.lv++;
                hero.exp = 0;
                count++;
                return CaculateExp(hero, getExp + j, count);
            }
            return count;
        }
        static void HeroDie(Hero hero)
        {
            int getInt;

            Console.Clear();
        DieMain:
            Console.WriteLine("\n사망하였습니다...");

            Console.WriteLine("1.나가기");
            Console.WriteLine("번호을 입력해 주세요");
            Console.Write(">>");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                Console.Clear();
                Console.WriteLine("종료합니다. 아무키나 눌러주세요");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                goto DieMain;
            }
        }
    }
}
