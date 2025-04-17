using System.Dynamic;

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

        FirstMain:
            Console.Clear();
            Console.WriteLine("환영");
            Console.WriteLine("이름 입력");
            hero.name = (Console.ReadLine());


            Console.Clear();
            Console.WriteLine("직업");
            Console.WriteLine("전사, 도적");

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
                Console.WriteLine("잘못된 값 입력함");
                goto FirstMain;
            }


        SecondMain:
            Console.Clear();
            Console.WriteLine("입력");
            Console.WriteLine("1.스텟  2.인벤  3.상점  4.던전 입장  5.휴식");
            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                ShowStats(hero);
                goto SecondMain;
            }
            else if (getInt == 2)
            {
                ShowInventory(hero);
                goto SecondMain;
            }
            else if (getInt == 3)
            {
                ShowStore(sword, armor, hero);
                goto SecondMain;
            }
            else if (getInt == 4)
            {
                int result;
                Console.Clear();
                Console.WriteLine("던전 Lv1\t방어력 5이상 권장\n");
                Console.WriteLine("던전 Lv2\t방어력 11이상 권장\n");
                Console.WriteLine("던전 Lv3\t방어력 17이상 권장\n");
                Console.WriteLine("0.나가기");

                getInt = Input(Console.ReadLine());

                if (getInt == 1)
                {
                    GoToDungeon(5, hero.stats[1], hero);
                    goto SecondMain;
                }
                else if (getInt == 2)
                {
                    GoToDungeon(11, hero.stats[1], hero);
                    goto SecondMain;
                }
                else if (getInt == 3)
                {
                    GoToDungeon(17, hero.stats[1], hero);
                    goto SecondMain;
                }
                else
                {
                    goto SecondMain;
                }
            }
            else if (getInt == 5)
            {
                Console.Clear();
                Console.WriteLine("500골드를 내고 휴식을 취하시겠습니까?\n");
                Console.WriteLine("현재 보유 골드 : {0}\n", hero.gold);
                Console.WriteLine("1.예  2.아니요\n");

                getInt = Input(Console.ReadLine());

                if (getInt == 1)
                {
                    GetSomeRest(hero);
                }
                else if (getInt == 2)
                    goto SecondMain;
                else
                    goto SecondMain;
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
            sword.description = ["쉽게 볼 수 있는 낡은 검 입니다.", "어디선가 사용됐던거 같은 도끼입니다.", "스파르타의 전사들이 사용했다는 전설의 창입니다."];
            sword.value = ["600", "1500", "3000"];
            sword.sellValue = ["300", "750", "1500"];
            sword.atk = [2, 5, 7];
            sword.equipped = ["N", "N", "N"];
            sword.type = [1, 1, 1];

            armor.name = ["수련자 갑옷", "무쇠 갑옷", "스파르타의 갑옷"];
            armor.description = ["수련에 도움을 주는 갑옷입니다.", "무쇠로 만들어져 튼튼한 갑옷입니다.", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."];
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
        StatsAgain:
            Console.Clear();
            Console.WriteLine("\t[스텟]\t\n");
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
            Console.WriteLine("입력해주세요");

            getInt = Input(Console.ReadLine());

            if (getInt == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 값 입력함");
                goto StatsAgain;
            }
        }

        static void ShowInventory(Hero hero)
        {
        InventoryMain:
            int getInt = 0;
            string getString;
            int invListCount = hero.inventoryList.Count;
            Console.Clear();
            Console.WriteLine("\t[인벤토리]\t\n");

            PrintInvenList(hero, invListCount, 1);

            Console.WriteLine("\t\t\t골드 : {0}G", hero.gold);

            Console.WriteLine("\n1.장착관리  0.나가기");
            Console.WriteLine("입력해주세요");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                InventoryManagement(hero);
                goto InventoryMain;
            }
            else if (getInt == 0)
            {
                return;
            }
            else
            {
                goto InventoryMain;
            }
        }

        static void InventoryManagement(Hero hero)
        {
        InvManMain:
            int getInt = 0;
            string getString;
            int invListCount = hero.inventoryList.Count;
            Console.Clear();
            Console.WriteLine("\t[인벤토리 장착관리]\t\n");

            PrintInvenList(hero, invListCount, 2);

            Console.WriteLine("\n장착할 장비의 번호를 선택해주세요  0.나가기");
            Console.WriteLine("입력해주세요");

            getInt = Input(Console.ReadLine());

            if (getInt > 0 && getInt <= invListCount)
            {
                InventoryEquip(hero, getInt);
                goto InvManMain;
            }
            else if (getInt == 0)
            {
                return;
            }
            else
            {
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
        StoreAgain:
            Console.Clear();
            Console.WriteLine("\t[상점]\t\n");

            PrintStoreItem(sword, armor, swordCount, armorCount, 1);
            Console.WriteLine("\n\t\t\t골드 : {0}G", hero.gold);

            Console.WriteLine("\n1.아이템 구매  2.아이템 판매  3.인벤토리  0.나가기");
            Console.WriteLine("입력해주세요");

            getInt = Input(Console.ReadLine());

            if (getInt == 1)
            {
                //아이템 구매 창
                int[] iNumArr = StorePurchase(sword, armor);

                if (iNumArr[0] == 0)
                {
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
                        Console.WriteLine("구매가 완료되었습니다.  0.나가기");
                        Console.WriteLine("입력해주세요");

                        getInt = Input(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.  0.나가기");
                        Console.WriteLine("입력해주세요");

                        getInt = Input(Console.ReadLine());
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
                        Console.WriteLine("\n구매가 완료되었습니다.  0.나가기");
                        Console.WriteLine("입력해주세요");

                        getInt = Input(Console.ReadLine());

                        if (getInt == 0)
                        {
                            goto StoreAgain;
                        }
                        else
                        {
                            goto StoreAgain;
                        }
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.  0.나가기");
                        Console.WriteLine("입력해주세요");

                        getInt = Input(Console.ReadLine());

                        if (getInt == 0)
                        {
                            goto StoreAgain;
                        }
                        else
                        {
                            goto StoreAgain;
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
                Console.WriteLine("잘못된 값 입력함");
                goto StoreAgain;
            }
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
        PurchaseAgain:
            Console.Clear();
            Console.WriteLine("\t[상점]\t\n");

            PrintStoreItem(sword, armor, swordCount, armorCount, 2);

            Console.WriteLine("\n구매할 아이템 번호 선택  0.나가기");
            Console.WriteLine("입력해주세요");

            getInt = Input(Console.ReadLine());

            if (getInt > 0 && getInt <= swordCount)
            {
                if (sword.value[getInt - 1] == "구매한 상품입니다.")
                {
                    Console.Clear();
                    Console.WriteLine("\n이미 구매한 상품입니다.");
                    Console.WriteLine("\n0.나가기");
                    Console.WriteLine("입력해주세요");


                    getInt = Input(Console.ReadLine());

                    if (getInt == 0)
                    {
                        goto PurchaseAgain;
                    }
                    else
                    {
                        goto PurchaseAgain;
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
                    Console.WriteLine("\n이미 구매한 상품입니다.");
                    Console.WriteLine("\n0.나가기");
                    Console.WriteLine("입력해주세요");

                    getInt = Input(Console.ReadLine());

                    if (getInt == 0)
                    {
                        goto PurchaseAgain;
                    }
                    else
                    {
                        goto PurchaseAgain;
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
                Console.WriteLine("잘못된 값 입력함");
                goto PurchaseAgain;
            }

        }
        static int PurchaseGear(int iNum, Sword sword)
        {
            int getInt = 0;
            string getString;
        PurchaseAgain:
            Console.Clear();
            Console.WriteLine("\t[구매]\t\n");

            Console.WriteLine("- {0}\t | {1}\t | {2}\t | {3}", sword.name[iNum - 1], sword.atk[iNum - 1], sword.description[iNum - 1], sword.value[iNum - 1]);


            Console.WriteLine("\n구매하시겠습니까?\n1.예  2.아니요");

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
                Console.WriteLine("잘못된 값 입력함");
                goto PurchaseAgain;
            }
        }
        static int PurchaseGear(int iNum, Armor armor)
        {
            int getInt = 0;
            string getString;
        PurchaseAgain:
            Console.Clear();
            Console.WriteLine("\t[구매]\t\n");

            Console.WriteLine("- {0}\t | {1}\t | {2}\t | {3}", armor.name[iNum - 1], armor.def[iNum - 1], armor.description[iNum - 1], armor.value[iNum - 1]);


            Console.WriteLine("\n구매하시겠습니까?\n1.예  2.아니요");

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
                Console.WriteLine("잘못된 값 입력함");
                goto PurchaseAgain;
            }
        }

        static void SellGear(Hero hero)
        {
        SellInventoryMain:
            int getInt2 = 0;
            string getString2;
            int invListCount = hero.inventoryList.Count;
            Console.Clear();
            Console.WriteLine("\t[상점 판매]\t\n");

            PrintInvenList(hero, invListCount, 2);

            Console.WriteLine("\n판매할 물건의 번호를 입력해주세요  0.나가기");
            Console.WriteLine("입력해주세요");

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
                Console.WriteLine("\n판매가 완료되었습니다.  0.나가기");
                Console.WriteLine("입력해주세요");

                getInt2 = Input(Console.ReadLine());

                if (getInt2 == 0)
                {
                    goto SellInventoryMain;
                }
            }
            else if (getInt2 == 0)
            {
                return;
            }
            else
            {
                return;
            }
        }

        static void InventoryEquip(Hero hero, int getInt)
        {
        EquipMain:
            int getInt2;
            string getString2;
            Console.Clear();
            Console.WriteLine("\t[인벤토리 장착관리]\t\n");

            Console.WriteLine("- {0}\t   | {1}\t | {2}", hero.inventoryList[getInt - 1][0], hero.inventoryList[getInt - 1][1], hero.inventoryList[getInt - 1][2]);

            if (hero.inventoryList[getInt - 1][4] == "N")
            {
                Console.WriteLine("\n1.장착  0.나가기");
                Console.WriteLine("입력해주세요");

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
                    goto EquipMain;
                }
            }
            else
            {
                Console.WriteLine("\n1.해제  0.나가기");
                Console.WriteLine("입력해주세요");

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
            if (printType == 1)
            {
                for (int i = 0; i < invListCount; i++)
                {
                    if (hero.inventoryList[i][4] == "N")
                    {
                        if (hero.inventoryList[i][5] == "1")
                        {
                            Console.WriteLine("- {0}\t   | 공격력 +{1}\t | {2}", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                        else
                        {
                            Console.WriteLine("- {0}\t   | 방어력 +{1}\t | {2}", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }

                    }
                    else if (hero.inventoryList[i][4] == "E")
                    {
                        if (hero.inventoryList[i][5] == "1")
                        {
                            Console.WriteLine("- [E]{0}\t   | 공격력 +{1}\t | {2}", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                        else
                        {
                            Console.WriteLine("- [E]{0}\t   | 방어력 +{1}\t | {2}", hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                        }
                    }
                }
            }
            else if (printType == 2)
            {
                for (int i = 0; i < invListCount; i++)
                {
                    if (hero.inventoryList[i][4] == "N")
                    {
                        Console.WriteLine("-{0} {1}\t   | {2}\t | {3}", i + 1, hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                    }
                    else if (hero.inventoryList[i][4] == "E")
                    {
                        Console.WriteLine("-{0} [E]{1}\t   | {2}\t | {3}", i + 1, hero.inventoryList[i][0], hero.inventoryList[i][1], hero.inventoryList[i][2]);
                    }
                }
            }
        }
        static void PrintStoreItem(Sword sword, Armor armor, int swordCount, int armorCount, int printType)
        {
            if (printType == 1)
            {
                for (int i = 0; i < swordCount; i++)
                {
                    Console.WriteLine("- {0}\t   | 공격력 +{1}\t | {2}\t | {3}", sword.name[i], sword.atk[i], sword.description[i], sword.value[i]);
                }
                for (int i = 0; i < armorCount; i++)
                {
                    Console.WriteLine("- {0}\t   | 방어력 +{1}\t | {2}\t | {3}", armor.name[i], armor.def[i], armor.description[i], armor.value[i]);
                }
            }
            else if (printType == 2)
            {
                for (int i = 0; i < swordCount; i++)
                {
                    Console.WriteLine("-{0} {1}\t | 공격력 +{2}\t | {3}\t | {4}", i + 1, sword.name[i], sword.atk[i], sword.description[i], sword.value[i]);
                }
                for (int i = 0; i < armorCount; i++)
                {
                    Console.WriteLine("-{0} {1}\t | 방어력 +{2}\t | {3}\t | {4}", i + 1 + swordCount, armor.name[i], armor.def[i], armor.description[i], armor.value[i]);
                }
            }
        }
        static void GetSomeRest(Hero hero)
        {
            int getInt;
            if(hero.gold >= 500)
            {
                hero.stats[2] = 100;
                hero.gold -= 500;
                Console.Clear();
                Console.WriteLine("휴식을 충분히 취했습니다.\n");
                Console.WriteLine("체력이 100으로 회복되었습니다.");

                Console.WriteLine("0.나가기");
                Console.WriteLine("버튼을 눌러주세요");

                getInt = Input(Console.ReadLine());
                if (getInt == 0)
                {
                    return;
                }
                else
                {
                    return;
                } 
            }
            else
            {
                Console.Clear();
                Console.WriteLine("골드가 부족합니다.\n");
                Console.WriteLine("0.나가기");
                Console.WriteLine("버튼을 눌러주세요");

                getInt = Input(Console.ReadLine());
                if (getInt == 0)
                {
                    return;
                }
                else
                {
                    return;
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
                //goto FirstMain;
                return 0;
            }
        }
        static void GoToDungeon(int dunLevel, int myDefence, Hero hero)
        {
            Random rand = new Random();
            int iValue = 0, result = 0, iNum = 0;
            int goldGain = 0;
            int damage = dunLevel - myDefence;
            int[] iLevUP = [0, 0];
            if (myDefence > dunLevel)
            {
                iValue = rand.Next(20, 36);
                hero.stats[2] -= iValue + damage;
                iLevUP = GetExp(hero, dunLevel);
                result = 1;
            }
            else if (myDefence <= dunLevel)
            {
                iValue = rand.Next(0, 10);
                if (iValue < 4)
                {
                    //던전공략실패
                    hero.stats[2] /= 2;
                    hero.stats[2] -= iValue + damage;
                    
                    result = 0;
                }
                else
                {
                    iValue = rand.Next(20, 36);
                    hero.stats[2] -= iValue + damage;
                    iLevUP = GetExp(hero, dunLevel);
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
                Console.WriteLine("던전을 클리어 하였습니다.\n");

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
                Console.WriteLine("버튼을 눌러주세요");

                iNum = Input(Console.ReadLine());

                if (iNum == 0)
                {
                    return;
                }
                else return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("던전을 클리어 하지 못했습니다.\n");

                Console.WriteLine("체력 : {0} -> {1}", hero.stats[2] + iValue + damage, hero.stats[2]);
                Console.WriteLine("Gold : {0} -> {1}", hero.gold - goldGain, hero.gold);

                Console.WriteLine("\n0.확인\n");
                Console.WriteLine("버튼을 눌러주세요");

                iNum = Input(Console.ReadLine());

                if (iNum == 0)
                {
                    return;
                }
                else return;
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

    }
}
