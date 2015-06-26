using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    AracTeknikServisEntities1 arac = new AracTeknikServisEntities1();

    protected void Page_Load(object sender, EventArgs e)
    {
        goster();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool aktif = false;
        int id = Convert.ToInt32(TextBox1.Text);
        string plk = TextBox2.Text;
        int mdl = Convert.ToInt32(TextBox3.Text);
        string rnk = TextBox4.Text;
        if (CheckBox1.Checked) aktif = true;

        ekle(id,plk,mdl,rnk, aktif); 
        goster();
        
    }

    public void sil(string plaka)
    {  
        try
        {
           var kytsil=(from k in arac.Aracs where k.Plaka==plaka select k).FirstOrDefault();
                       
            arac.Aracs.Remove(kytsil);
            arac.SaveChanges();
        }
        catch (Exception)
        {
            Response.Write("HATA");
        }
       
    }

    public void guncelle(int sahipid, string plaka, int modelno, string renk, bool aktif)
    {
        try
        {
            Arac aracguncelle = (from k in arac.Aracs where k.Plaka == plaka select k).FirstOrDefault();
            aracguncelle.AracSahibiID = sahipid;
            aracguncelle.ModelNo = modelno;
            aracguncelle.Renk = renk;
            aracguncelle.active = aktif;

            arac.SaveChanges();

        }
        catch (Exception)
        {
            Response.Write("HATA");
        }
    }

    public void ekle(int sahipid,string plaka,int modelno,string renk,bool aktif)
    { 
        try
        {
            Arac araclar = new Arac
            {
                AracSahibiID=sahipid,
                Plaka=plaka,
                ModelNo=modelno,
                Renk=renk,
                active=aktif

            };
           arac.Aracs.Add(araclar);
           arac.SaveChanges();
        }
        catch (Exception)
        {
            Response.Write("HATA");
        }
        
    }

    public void goster()
    {
        //Burası istediğimiz alanlar icin
        //var oku = from c in arac.Aracs
        //          select new
        //          {
        //              c.AracSahibiID,
        //              c.Plaka,
        //              c.ModelNo,
        //              c.Renk,
        //              c.active
        //          };
        //GridView1.DataSource = oku.ToList();


       //******* burası tüm alanlar için
       GridView1.DataSource = arac.Aracs.ToList();
       GridView1.DataBind();


        //**********************Sadece belirli özelliğe göre verileri getirmesi için

        //var rengeGore = from rr in arac.Aracs where rr.Renk=="Lacivert"
        //                select new
        //                {
        //                    rr.AracSahibiID,
        //                    rr.Plaka,
        //                    rr.ModelNo,
        //                    rr.Renk,
        //                    rr.active
        //                };
        //GridView1.DataSource = rengeGore.ToList();
        //GridView1.DataBind();


    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        bool aktif = false;
        int id = Convert.ToInt32(TextBox1.Text);
        string plk = TextBox2.Text;
        int mdl = Convert.ToInt32(TextBox3.Text);
        string rnk = TextBox4.Text;
        if (CheckBox1.Checked) aktif = true;

        guncelle(id, plk, mdl, rnk, aktif);
        goster();
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        sil(TextBox5.Text);
        goster();
        
    }
}