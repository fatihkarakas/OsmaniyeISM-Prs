﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PersonelEkle : System.Web.UI.Page
{
    public DataContext dc = new DataContext();
    public GenelAyarlar g = new GenelAyarlar();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            KartNo.Focus();
        }

    }

    protected void Kontrol_Et(object sender, EventArgs e)
    {
        Ad.Text = "";
        Soyad.Text = "";
        KartNumber.Text = "";
        KartId.Text = "";
        SicilNo.Text = "";
        KanGrup.SelectedIndex = 0;
        BasTarih.Text = "";

        if (KartNo.Text != null || KartNo.Text.Count() > 5)
        {
            try
            {
                var Pers = dc.personel.Where(x => x.kartid == KartNo.Text).FirstOrDefault();
                if (Pers == null)
                {

                    KartId.Text = KartNo.Text;
                    KartId.Enabled = false;
                    int cardno = Convert.ToInt32(dc.personel.Max(x => x.kartno));
                    cardno++;
                    var cardno1 = cardno.ToString();
                    if (cardno1.Length < 5)
                    {
                        switch (cardno1.Length)
                        {
                            case 4:
                                cardno1 = "0" + cardno1.ToString();
                                break;
                            case 3:
                                cardno1 = "00" + cardno1.ToString();
                                break;
                            case 2:
                                cardno1 = "000" + cardno1.ToString();
                                break;
                            default:
                                break;


                        }
                    }
                    KartNumber.Text = cardno1.ToString();
                    KartNumber.Enabled = false;
                }
                else
                {
                    Ad.Text = Pers.ad;
                    Soyad.Text = Pers.soyad;
                    KartNumber.Text = Pers.kartno;
                    KartId.Text = Pers.kartid;
                    SicilNo.Text = Pers.sicilno;
                    if (Pers.servisid != null)
                    {
                        BaskanlikDrop.SelectedValue = Pers.servisid.ToString();
                    }
                    BasTarih.Text = string.Format("{0:yyyy/MM/dd}", Pers.isegiristarihi);
                    KartNumber.Enabled = false;
                    KartId.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                HataMsj.Visible = true;
                msj.InnerText = "Hata oluştu, hata kodu: " + ex.Message.ToString();
                return;
            }
        }
        else
        {
            HataMsj.Visible = true;
            msj.InnerText = "Lütfen kart formatı doğru formatta giriniz";
            return;
        }
        FormGorunum.Visible = true;
        KartNo.Text = "";
        KartNo.Focus();
    }

    protected void PersEkleBtn_Click(object sender, EventArgs e)
    {
        var KartIDD = KartId.Text;
        if (Ad.Text == "" || Soyad.Text == "" || KartNumber.Text == "" || KartId.Text == "" || SicilNo.Text == "")
        {
            HataMsj.Visible = true;
            msj.InnerText = "Lütfen Personel bilgilerini doldurunuz";
            return;
        }
        var Pers = dc.personel.Where(x => x.kartid == KartIDD).FirstOrDefault();
        if (Pers != null)
        {
            try
            {
                Pers.ad = Ad.Text;
                Pers.soyad = Soyad.Text;
                Pers.sicilno = SicilNo.Text;
                Pers.isegiristarihi = Convert.ToDateTime(BasTarih.Text);
                Pers.servisid = Convert.ToInt32(BaskanlikDrop.SelectedValue);
                Pers.kangrubu = KanGrup.SelectedItem.Text;
                dc.Entry(Pers).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
            }
            catch (Exception ex)
            {

                HataMsj.Visible = true;
                msj.InnerText = "Hata oluştu " + ex.Message.ToString();
                return;
            }
           

        }
        else
        {
            try
            {
                personel ps = new personel();

                ps.ad = Ad.Text;
                ps.soyad = Soyad.Text;
                ps.birimid = Convert.ToInt32(BaskanlikDrop.SelectedValue);
                ps.sicilno = SicilNo.Text;
                ps.kartno = KartNumber.Text;
                ps.kartid = KartId.Text;
                ps.isegiristarihi = Convert.ToDateTime(BasTarih.Text);
                ps.durum = "Aktif";
                ps.ceptel = "";
                ps.personelturu = "Personel";
                ps.resim = null;
                ps.email = "";
                ps.cinsiyet = "";
                ps.kartfc = "0";

                
                dc.personel.Add(ps);
                dc.SaveChanges();
            }
            catch (Exception ex)
            {
                HataMsj.Visible = true;
                msj.InnerText = "Hata oluştu " + ex.Message.ToString();
                return;
            }
            try
            {
                ListBox hatalar = new ListBox();
                string terminal = "10.80.15.220";
                var baglan = g.sta_ConnectTCP(hatalar, terminal, "4370", "1");
                g.axCZKEM1.EnableDevice(1, false);
                g.axCZKEM1.SetStrCardNumber(KartNumber.Text);
                g.axCZKEM1.SSR_SetUserInfo(1, KartNumber.Text, Ad.Text, "", 0, true);


            }
            catch (Exception ex)
            {
                HataMsj.Visible = true;
                msj.InnerText = "Hata oluştu " + ex.Message.ToString();
                return;
            }
            finally
            {
                g.axCZKEM1.EnableDevice(1, true);
            }

        }
        HataMsj.Visible = true;
        msj.InnerText = Ad.Text +   " " +Soyad.Text +" için Personel bilgileri başarı ile güncellendi veya kayıt edildi";
        HataMsj.Attributes.Add("class", "alert alert-success");
        Ad.Text = "";
        Soyad.Text = "";
        KartNumber.Text = "";
        KartId.Text = "";
        SicilNo.Text = "";
        KanGrup.SelectedIndex = 0;
        BasTarih.Text = "";
        KartNo.Focus();

    }
}