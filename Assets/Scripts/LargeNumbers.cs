using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LargeNumbers : MonoBehaviour
{
    public Text MoneyText;
    double a = 10;

    void Start()
    {
        StartCoroutine(TestRoutine());
    }
    IEnumerator TestRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        int rand = Random.Range(100, 1000);
        a += rand;
        MoneyText.text = ChangeFormat(a);
        StartCoroutine(TestRoutine());
    }
    public void AddMoney()
    {
        a = a * 10;
        MoneyText.text = ChangeFormat(a);       
    }
    // Update is called once per frame
   
    void FixedUpdate()
    {
       
    }
    public string ChangeFormat(double target)
    {
        string haveGold = target.ToString("0");
        if (double.IsInfinity(target) == true)
            return "infinity";
        string[] unit = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L","M","N","O","P","Q","R","S","T","U",
        "V","W","X","Y","Z","Aa","Ab","Ac","Ad","Ae","Af","Ag","Ah","Ai","Aj","Ak","Al","Am","An","Ao","Ap","Aq","Ar","As","At","Au","Av","Aw","Ax","Ay","Az",
        "Ba","Bb","Bc","Bd","Be","Bf","Bg","Bh","Bi","Bj","Bk","Bl","Bm","Bn","Bo","Bp","Bq","Br","Bs","Bt","Bu","Bv","Bw","Bx","By","Bz",
        "Ca","Cb","Cc","Cd","Ce","Cf","Cg","Ch","Ci","Cj","Ck","Cl","Cm","Cn","Co","Cp","Cq","Cr","Cs","Ct","Cu","Cv","Cw","Cx","Cy","Cz",
        "Da","Db","Dc","Dd","De","Df","Dg","Dh","Di","Dj","Dk","Dl","Dm","Dn","Do","Dp","Dq","Dr","Ds","Dt","Du","Dv","Dw","Dx","Dy","Dz",
        "Ea","Eb","Ec","Ed","Ee","Ef","Eg","Eh","Ei","Ej","Ek","El","Em","En","Eo","Ep","Eq","Er","Es","Et","Eu","Ev","Ew","Ex","Ey","Ez"};


        int[] cVal = new int[unit.Length];
        int index = 0;
        while (true)
        {
            string last4 = "";
            if (haveGold.Length >= 4)
            {
                last4 = haveGold.Substring(haveGold.Length - 4);
                int intLast4 = int.Parse(last4);

                cVal[index] = intLast4 % 1000;

                haveGold = haveGold.Remove(haveGold.Length - 3);
            }
            else
            {
                cVal[index] = int.Parse(haveGold);
                break;
            }

            index++;
        }

        if (index > 0)
        {
            int r = cVal[index] * 1000 + cVal[index - 1];
            string temp = (r / 1000f).ToString("N3");

            //return string.Format("{0:#.###} {1}", (float)r / 1000f, unit[index]);                        
            return string.Format("{0} {1}", temp, unit[index]);
        }

        return haveGold;
    }
}
