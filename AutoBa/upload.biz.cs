using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace AutoBa
{
    public class UploadBiz
    {
        #region 病案首页 & 出院小结
        /// <summary>
        /// 病案首页 & 出院小结
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetPatList(List<EntityParm> dicParm)
        {
            string SqlBa = string.Empty;
            string SqlReg = string.Empty;
            string SqlJs = string.Empty;
            int n = 0;
            List<EntityPatUpload> data = new List<EntityPatUpload>();
            SqlHelper svcBa = null;
            SqlHelper svc = null;
            bool isExisitBa = false;
            bool isUploadparm = false;
            try
            {
                #region Sql 病案首页信息
                svcBa = new SqlHelper(EnumBiz.baDB);
                svc = new SqlHelper(EnumBiz.onlineDB);
                SqlBa = @"select 
                                        a.ftimes as FTIMES,
                                        a.fid,
                                        a.fzyid,
                                        a.fcydate,
                                        '' as JZJLH,
                                        '' as FWSJGDM,
                                        '' as FBGLX,
                                        a.fidcard,
                                        a.FFBBHNEW,a.FFBNEW,
                                        a.FASCARD1,
                                        a.FPRN,
                                        a.FNAME,a.FSEXBH,
                                        a.FSEX,a.FBIRTHDAY,
                                        a.FAGE,a.fcountrybh,
                                        a.fcountry,a.fnationalitybh,
                                        a.fnationality,a.FCSTZ,
                                        a.FRYTZ,a.FBIRTHPLACE,
                                        a.FNATIVE,a.FIDCard,
                                        a.FJOB,a.FSTATUSBH,
                                        a.FSTATUS,a.FCURRADDR,
                                        a.FCURRTELE,a.FCURRPOST,
                                        a.FHKADDR,a.FHKPOST,
                                        a.FDWNAME,a.FDWADDR,
                                        a.FDWTELE,a.FDWPOST,
                                        a.FLXNAME,a.FRELATE,
                                        a.FLXADDR,a.FLXTELE,
                                        a.FRYTJBH,a.FRYTJ,
                                        a.FRYDATE,a.FRYTIME,
                                        a.FRYTYKH,a.FRYDEPT,
                                        a.FRYBS,a.FZKTYKH,
                                        a.FZKDEPT,a.FZKTIME,
                                        a.FCYDATE,a.FCYTIME,
                                        a.FCYTYKH,a.FCYDEPT,
                                        a.FCYBS,a.FDAYS,
                                        a.FMZZDBH,a.FMZZD,
                                        a.FMZDOCTBH,a.FMZDOCT,
                                        a.FJBFXBH,a.FJBFX,
                                        a.FYCLJBH,a.FYCLJ,
                                        a.FQJTIMES,a.FQJSUCTIMES,
                                        a.FPHZD,a.FPHZDNUM,
                                        a.FPHZDBH,a.FIFGMYWBH,
                                        a.FIFGMYW,a.FGMYW,
                                        a.FBODYBH,a.FBODY,
                                        a.FBLOODBH,a.FBLOOD,
                                        a.FRHBH,a.FRH,
                                        a.FKZRBH,a.FKZR,
                                        a.FZRDOCTBH,a.FZRDOCTOR,
                                        a.FZZDOCTBH,a.FZZDOCT,
                                        a.FZYDOCTBH,a.FZYDOCT,
                                        a.FNURSEBH,a.FNURSE,
                                        a.FJXDOCTBH,a.FJXDOCT,
                                        a.FSXDOCTBH,a.FSXDOCT,
                                        a.FBMYBH,
                                        a.FBMY,a.FQUALITYBH,
                                        a.FQUALITY,a.FZKDOCTBH,
                                        a.FZKDOCT,a.FZKNURSEBH,
                                        a.FZKNURSE,a.FZKRQ,
                                        a.FLYFSBH,a.FLYFS,a.FYZOUTHOSTITAL,
                                        a.FSQOUTHOSTITAL,a.FISAGAINRYBH,
                                        a.FISAGAINRY,a.FISAGAINRYMD,
                                        a.FRYQHMDAYS,a.FRYQHMHOURS,
                                        a.FRYQHMMINS,a.FRYQHMCOUNTS,
                                        a.FRYHMDAYS,a.FRYHMHOURS,
                                        a.FRYHMMINS,a.FRYHMCOUNTS,a.FSUM1,
                                        a.FZFJE,a.FZHFWLYLF,a.FZHFWLCZF,a.FZHFWLHLF,
                                        a.FZHFWLQTF,a.FZDLBLF,a.FZDLSSSF,
                                        a.FZDLYXF,a.FZDLLCF,a.FZLLFFSSF,a.FZLLFWLZWLF,
                                        a.FZLLFSSF,a.FZLLFMZF,
                                        a.FZLLFSSZLF,a.FKFLKFF,a.FZYLZF,
                                        a.FXYF,a.FXYLGJF,a.FZCHYF,
                                        a.FZCYF,a.FXYLXF,a.FXYLBQBF,
                                        a.FXYLQDBF,a.FXYLYXYZF,a.FXYLXBYZF,
                                        a.FHCLCJF,a.FHCLZLF,a.FHCLSSF,
                                        a.FQTF,a.FZYF,a.FZKDATE,
                                        a.FJOBBH,a.FZHFWLYLF01,a.FZHFWLYLF02,
                                        a.FZYLZDF,a.FZYLZLF,a.FZYLZLF01,a.FZYLZLF02,
                                        a.FZYLZLF03,a.FZYLZLF04,a.FZYLZLF05,a.FZYLZLF06,a.FZYLQTF,
                                        a.FZCLJGZJF,a.FZYLQTF01,a.FZYLQTF02
                                        from tPatientVisit a where a.fzyid is not null ";
                #endregion

                #region SqlReg  查找住院记录

                SqlReg = @"select distinct t1.registerid_chr,
                                t1.patientid_chr as MZH,
                                d.lastname_vchr as xm,
                                d.birth_dat as birth,
                                d.sex_chr as sex,
                                d.idcard_chr,
                                d.homeaddress_vchr as YJDZ,
                                t1.inpatientid_chr as ipno,
                                t1.inpatientcount_int as rycs,
                                t1.deptid_chr as rydeptid,
                                t11.deptname_vchr as ryks,
                                c.outdeptid_chr as cydeptid,
                                c1.deptname_vchr as cyks,
                                to_char(t1.inpatient_dat, 'yyyymmdd') as RYRQ1,
                                to_char(c.outhospital_dat, 'yyyymmdd') as CYRQ1,
                                t1.inpatient_dat as RYSJ,
                                c.modify_dat as CYSJ,
                                rehis.emrinpatientid,
                                rehis.emrinpatientdate,
                                ee.lastname_vchr as jbr,
                                --dd.serno,
                                dd.status,
                                dd.uploaddate
                                from t_opr_bih_register t1
                                left join t_bse_deptdesc t11
                                on t1.deptid_chr = t11.deptid_chr
                                left join t_opr_bih_leave c
                                on t1.registerid_chr = c.registerid_chr
                                left join t_bse_deptdesc c1
                                on c.outdeptid_chr = c1.deptid_chr
                                left join t_opr_bih_registerdetail d
                                on t1.registerid_chr = d.registerid_chr
                                left join t_bse_hisemr_relation rehis
                                on t1.registerid_chr = rehis.registerid_chr
                                left join t_upload dd
                                on t1.registerid_chr = dd.registerid
                                left join t_bse_employee ee
                                on dd.opercode = ee.empno_chr
                                where c.status_int = 1 ";
                #endregion

                #region 结算记录

                SqlJs = @"select distinct a.registerid_chr,
                                            e.jzjlh,
                                            d.invoiceno_vchr,
                                            b.inpatientid_chr,
                                            c.status,
                                            c.firstSource
                              from  t_opr_bih_charge a
                              left join t_opr_bih_register b
                                on a.registerid_chr = b.registerid_chr
                                left join t_opr_bih_chargedefinv d
                                on a.chargeno_chr = d.chargeno_chr
                                left join t_ins_chargezy_csyb e
                                on a.registerid_chr = e.registerid_chr
                              left join t_upload c
                                on a.registerid_chr = c.registerid
                            where a.class_int = 2
                             and (a.operdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                  to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";
                #endregion

                #region 条件
                string strSubJs = string.Empty;
                List<IDataParameter> lstParm = new List<IDataParameter>();
                // 默认参数
                foreach (EntityParm po in dicParm)
                {
                    string keyValue = po.value;
                    switch (po.key)
                    {
                        case "queryDate":
                            IDataParameter parm1 = svc.CreateParm();
                            parm1.Value = keyValue.Split('|')[0] + " 00:00:00";
                            lstParm.Add(parm1);
                            IDataParameter parm2 = svc.CreateParm();
                            parm2.Value = keyValue.Split('|')[1] + " 23:59:59";
                            lstParm.Add(parm2);
                            break;
                        case "cardNo":
                            strSubJs += " and b.inpatientid_chr = '" + keyValue + "'";
                            break;
                        case "JZJLH":
                            strSubJs += " and e.jzjlh = '" + keyValue + "'";
                            break;
                        case "JZJLH1":
                            strSubJs += "  e.jzjlh in " + keyValue;
                            break;
                        case "chkStat":
                            isUploadparm = true;
                            break;
                        default:
                            break;
                    }
                }

                #endregion

                #region 赋值
                if (!string.IsNullOrEmpty(strSubJs))
                    SqlJs += strSubJs;

                DataTable dtJs = svc.GetDataTable(SqlJs, lstParm.ToArray());
                if (dtJs != null && dtJs.Rows.Count > 0)
                {
                    string ipnoStr = string.Empty;
                    string registeridStr = string.Empty;
                    List<string> lstReg = new List<string>();
                    List<string> lstIpno = new List<string>();
                    DataTable dtBa = null;
                    DataTable dtReg = null;
                    foreach (DataRow drJs in dtJs.Rows)
                    {
                        string registerid = drJs["registerid_chr"].ToString();
                        string ipno = drJs["inpatientid_chr"].ToString();
                        int uploadStatus = Function.Int(drJs["status"]);
                        int firstSource = Function.Int(drJs["firstSource"]);
                        //未上传，来源JH也属于未上传
                        if (isUploadparm)
                        {
                            if (uploadStatus == 1 && firstSource == 1 && firstSource == 2) //uploadStatus 1 已上传 1 病案  2 icare
                                continue;
                        }
                        //不显示来自icare数据
                        if (firstSource == 2)
                            continue;

                        if (lstReg.Contains(registerid))
                            continue;
                        lstReg.Add(registerid);
                        registeridStr += "'" + registerid + "',";

                        if (lstIpno.Contains(ipno))
                            continue;
                        ipnoStr += "'" + ipno + "',";
                        lstIpno.Add(ipno);
                    }

                    if (!string.IsNullOrEmpty(ipnoStr))
                    {
                        ipnoStr = ipnoStr.TrimEnd(',');
                        registeridStr = registeridStr.TrimEnd(',');
                        SqlBa += " and (a.fprn in (" + ipnoStr + ")" + " or a.fzyid in (" + ipnoStr + ")" + ")";
                        dtBa = svcBa.GetDataTable(SqlBa);

                        SqlReg += "and t1.registerid_chr in (" + registeridStr + ")";
                        dtReg = svc.GetDataTable(SqlReg);
                    }

                    foreach (DataRow drReg in dtReg.Rows)
                    {
                        isExisitBa = false;
                        string MZH = drReg["MZH"].ToString();
                        string emrinpatientid = drReg["emrinpatientid"].ToString();
                        string emrinpatientdate = Function.Datetime(drReg["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        string ipno = drReg["ipno"].ToString();
                        string registerid = drReg["registerid_chr"].ToString();
                        int rycs = Function.Int(drReg["rycs"].ToString());
                        string cydate = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd");
                        string cydate1 = Function.Datetime(drReg["cysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string cydate2 = Function.Datetime(drReg["cysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string rydate = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd");
                        string rydate1 = Function.Datetime(drReg["rysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string rydate2 = Function.Datetime(drReg["rysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string jzjlh = string.Empty;
                        string FPHM = string.Empty;
                        EntityPatUpload upVo = null;

                        #region 查找发票号
                        DataRow[] drrFPHM = dtJs.Select("registerid_chr = '" + registerid + "'");
                        if (drrFPHM.Length > 0)
                        {
                            jzjlh = drrFPHM[0]["jzjlh"].ToString();
                            foreach (DataRow drrF in drrFPHM)
                            {
                                FPHM += drrF["invoiceno_vchr"].ToString() + ",";
                            }
                            if (!string.IsNullOrEmpty(FPHM))
                            {
                                FPHM = FPHM.TrimEnd(',');
                            }
                        }
                        #endregion

                        #region  首页信息
                        DataRow[] drr = dtBa.Select("fprn =  '" + ipno + "' or fzyid = '" + ipno + "'");
                        if (drr.Length > 0)
                        {
                            foreach (DataRow drrBa in drr)
                            {
                                string fcydate = Function.Datetime(drrBa["fcydate"]).ToString("yyyy-MM-dd");
                                string frydate = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                int ftimes = Function.Int(drrBa["FTIMES"].ToString());

                                if (cydate == fcydate || cydate1 == fcydate || cydate2 == fcydate || rydate == frydate || rydate1 == frydate || rydate2 == frydate)
                                {
                                    upVo = new EntityPatUpload();
                                    upVo.fpVo = new EntityFirstPage();
                                    isExisitBa = true;

                                    #region 首页信息  来源病案
                                    upVo.fpVo.JZJLH = jzjlh;
                                    upVo.fpVo.FWSJGDM = drrBa["FWSJGDM"].ToString();
                                    upVo.fpVo.FFBBHNEW = drrBa["FFBBHNEW"].ToString();
                                    upVo.fpVo.FFBNEW = drrBa["FFBNEW"].ToString();
                                    if (drrBa["FASCARD1"] != DBNull.Value)
                                        upVo.fpVo.FASCARD1 = drrBa["FASCARD1"].ToString();
                                    else
                                        upVo.fpVo.FASCARD1 = "1";
                                    upVo.fpVo.FTIMES = Function.Int(drrBa["FTIMES"].ToString());
                                    upVo.fpVo.FPRN = drrBa["FPRN"].ToString();
                                    upVo.fpVo.FNAME = drrBa["FNAME"].ToString();
                                    upVo.fpVo.FSEXBH = drrBa["FSEXBH"].ToString();
                                    upVo.fpVo.FSEX = drrBa["FSEX"].ToString();
                                    upVo.fpVo.FBIRTHDAY = Function.Datetime(drrBa["FBIRTHDAY"]).ToString("yyyyMMdd");
                                    upVo.fpVo.FAGE = drrBa["FAGE"].ToString();
                                    upVo.fpVo.fcountrybh = drrBa["fcountrybh"].ToString();
                                    if (upVo.fpVo.fcountrybh == "")
                                        upVo.fpVo.fcountrybh = "-";
                                    upVo.fpVo.fcountry = drrBa["fcountry"].ToString();
                                    if (upVo.fpVo.fcountry == "")
                                        upVo.fpVo.fcountry = "-";
                                    upVo.fpVo.fnationalitybh = drrBa["fnationalitybh"].ToString();
                                    if (upVo.fpVo.fnationalitybh == "")
                                        upVo.fpVo.fnationalitybh = "-";
                                    upVo.fpVo.fnationality = drrBa["fnationality"].ToString();
                                    upVo.fpVo.FCSTZ = drrBa["FCSTZ"].ToString();
                                    upVo.fpVo.FRYTZ = drrBa["FRYTZ"].ToString();
                                    upVo.fpVo.FBIRTHPLACE = drrBa["FBIRTHPLACE"].ToString();
                                    upVo.fpVo.FNATIVE = drrBa["FNATIVE"].ToString();
                                    upVo.fpVo.FIDCard = drrBa["FIDCard"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FIDCard))
                                        upVo.fpVo.FIDCard = "无";
                                    upVo.fpVo.FJOB = drrBa["FJOB"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FJOB))
                                        upVo.fpVo.FJOB = "其他";
                                    upVo.fpVo.FSTATUS = drrBa["FSTATUS"].ToString().Trim();
                                    if (upVo.fpVo.FSTATUS == "已婚")
                                        upVo.fpVo.FSTATUSBH = "2";
                                    else if (upVo.fpVo.FSTATUS == "未婚")
                                        upVo.fpVo.FSTATUSBH = "1";
                                    else if (upVo.fpVo.FSTATUS == "丧偶")
                                        upVo.fpVo.FSTATUSBH = "3";
                                    else if (upVo.fpVo.FSTATUS == "离婚")
                                        upVo.fpVo.FSTATUSBH = "4";
                                    else
                                        upVo.fpVo.FSTATUSBH = "9";
                                    upVo.fpVo.FCURRADDR = drrBa["FCURRADDR"].ToString();
                                    upVo.fpVo.FCURRTELE = drrBa["FCURRTELE"].ToString();
                                    upVo.fpVo.FCURRPOST = drrBa["FCURRPOST"].ToString();
                                    upVo.fpVo.FHKADDR = drrBa["FHKADDR"].ToString();
                                    upVo.fpVo.FHKPOST = drrBa["FHKPOST"].ToString();
                                    upVo.fpVo.FDWNAME = drrBa["FDWNAME"].ToString();
                                    upVo.fpVo.FDWADDR = drrBa["FDWADDR"].ToString();
                                    upVo.fpVo.FDWTELE = drrBa["FDWTELE"].ToString();
                                    upVo.fpVo.FDWPOST = drrBa["FDWPOST"].ToString();
                                    upVo.fpVo.FLXNAME = drrBa["FLXNAME"].ToString();
                                    upVo.fpVo.FRELATE = drrBa["FRELATE"].ToString();
                                    if (upVo.fpVo.FRELATE.Length > 10)
                                        upVo.fpVo.FRELATE = upVo.fpVo.FRELATE.Substring(0, 10);
                                    upVo.fpVo.FLXADDR = drrBa["FLXADDR"].ToString();
                                    upVo.fpVo.FLXTELE = drrBa["FLXTELE"].ToString();
                                    upVo.fpVo.FRYTJBH = drrBa["FRYTJBH"].ToString();
                                    if (upVo.fpVo.FRYTJBH == "")
                                        upVo.fpVo.FRYTJBH = "-";
                                    upVo.fpVo.FRYTJ = drrBa["FRYTJ"].ToString();
                                    if (upVo.fpVo.FRYTJ == "")
                                        upVo.fpVo.FRYTJ = "-";
                                    upVo.fpVo.FRYDATE = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                    upVo.fpVo.FRYTIME = drrBa["FRYTIME"].ToString();
                                    upVo.fpVo.FRYTIME = Function.Datetime(drrBa["FRYTIME"]).ToString("HH:mm:ss");
                                    upVo.fpVo.FRYTYKH = drrBa["FRYTYKH"].ToString();
                                    upVo.fpVo.FRYDEPT = drrBa["FRYDEPT"].ToString();
                                    upVo.fpVo.FRYBS = drrBa["FRYBS"].ToString().Trim();
                                    if (upVo.fpVo.FRYBS == "")
                                        upVo.fpVo.FRYBS = upVo.fpVo.FRYDEPT;
                                    upVo.fpVo.FZKTYKH = drrBa["FZKTYKH"].ToString();
                                    upVo.fpVo.FZKDEPT = drrBa["FZKDEPT"].ToString();
                                    upVo.fpVo.FZKTIME = drrBa["FZKTIME"].ToString();
                                    upVo.fpVo.FZKTIME = Function.Datetime(drrBa["FZKTIME"]).ToString("HH:mm:ss");
                                    upVo.fpVo.FCYDATE = Function.Datetime(drrBa["FCYDATE"]).ToString("yyyy-MM-dd");

                                    upVo.fpVo.FCYTIME = drrBa["FCYTIME"].ToString();
                                    upVo.fpVo.FCYTIME = Function.Datetime(drrBa["FCYTIME"]).ToString("HH:mm:ss");
                                    upVo.fpVo.FCYTYKH = drrBa["FCYTYKH"].ToString();
                                    upVo.fpVo.FCYDEPT = drrBa["FCYDEPT"].ToString();
                                    upVo.fpVo.FCYBS = drrBa["FCYBS"].ToString().Trim();
                                    if (upVo.fpVo.FCYBS == "")
                                        upVo.fpVo.FCYBS = upVo.fpVo.FCYDEPT;
                                    TimeSpan ts = Function.Datetime(upVo.fpVo.FCYDATE) - Function.Datetime(upVo.fpVo.FRYDATE);
                                    upVo.fpVo.FDAYS = ts.Days.ToString();
                                    if (upVo.fpVo.FDAYS == "0")
                                        upVo.fpVo.FDAYS = "1";

                                    upVo.fpVo.FMZZDBH = drrBa["FMZZDBH"].ToString();
                                    upVo.fpVo.FMZZD = drrBa["FMZZD"].ToString();
                                    upVo.fpVo.FMZDOCTBH = drrBa["FMZDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCTBH))
                                        upVo.fpVo.FMZDOCTBH = "无";
                                    upVo.fpVo.FMZDOCT = drrBa["FMZDOCT"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FMZDOCT))
                                        upVo.fpVo.FMZDOCT = "无";
                                    upVo.fpVo.FJBFXBH = drrBa["FJBFXBH"].ToString();
                                    upVo.fpVo.FJBFX = drrBa["FJBFX"].ToString();
                                    upVo.fpVo.FYCLJBH = drrBa["FYCLJBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FYCLJBH))
                                        upVo.fpVo.FYCLJBH = "2";
                                    upVo.fpVo.FYCLJ = drrBa["FYCLJ"].ToString();
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FYCLJBH))
                                        upVo.fpVo.FYCLJ = "是";
                                    else
                                        upVo.fpVo.FYCLJ = "否";
                                    upVo.fpVo.FQJTIMES = drrBa["FQJTIMES"].ToString();
                                    upVo.fpVo.FQJSUCTIMES = drrBa["FQJSUCTIMES"].ToString();
                                    if (!string.IsNullOrEmpty(upVo.fpVo.FQJTIMES) && string.IsNullOrEmpty(upVo.fpVo.FQJSUCTIMES))
                                    {
                                        upVo.fpVo.FQJSUCTIMES = upVo.fpVo.FQJTIMES;
                                    }
                                    upVo.fpVo.FPHZD = drrBa["FPHZD"].ToString();
                                    if (upVo.fpVo.FPHZD.Length > 100)
                                        upVo.fpVo.FPHZD = upVo.fpVo.FPHZD.Substring(0, 100);

                                    if (drrBa["FPHZDNUM"].ToString().Trim() != "")
                                        upVo.fpVo.FPHZDNUM = drrBa["FPHZDNUM"].ToString();
                                    else
                                        upVo.fpVo.FPHZDNUM = "-";

                                    if (drrBa["FPHZDBH"].ToString().Trim() != "")
                                        upVo.fpVo.FPHZDBH = drrBa["FPHZDBH"].ToString();
                                    else
                                        upVo.fpVo.FPHZDBH = "0";

                                    upVo.fpVo.FIFGMYWBH = drrBa["FIFGMYWBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FIFGMYWBH))
                                        upVo.fpVo.FIFGMYWBH = "1";
                                    if (drrBa["FIFGMYW"].ToString() != "")
                                        upVo.fpVo.FIFGMYW = drrBa["FIFGMYW"].ToString();
                                    else
                                        upVo.fpVo.FIFGMYW = "-";
                                    if (drrBa["FGMYW"].ToString() != "")
                                        upVo.fpVo.FGMYW = drrBa["FGMYW"].ToString();
                                    else
                                        upVo.fpVo.FGMYW = "-";
                                    if (drrBa["FBODYBH"].ToString().Trim() != "")
                                        upVo.fpVo.FBODYBH = drrBa["FBODYBH"].ToString();
                                    else
                                        upVo.fpVo.FBODYBH = "2";
                                    if (drrBa["FBODY"].ToString().Trim() != "")
                                        upVo.fpVo.FBODY = drrBa["FBODY"].ToString();
                                    else
                                        upVo.fpVo.FBODY = "否";
                                    upVo.fpVo.FBLOODBH = drrBa["FBLOODBH"].ToString();
                                    upVo.fpVo.FBLOOD = drrBa["FBLOOD"].ToString();
                                    upVo.fpVo.FRHBH = drrBa["FRHBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FRHBH))
                                        upVo.fpVo.FRHBH = "4";
                                    upVo.fpVo.FRH = drrBa["FRH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FRH))
                                        upVo.fpVo.FRH = "未查";
                                    upVo.fpVo.FKZRBH = drrBa["FKZRBH"].ToString();
                                    upVo.fpVo.FKZR = drrBa["FKZR"].ToString();
                                    upVo.fpVo.FZRDOCTBH = drrBa["FZRDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZRDOCTBH))
                                        upVo.fpVo.FZRDOCTBH = "-";
                                    upVo.fpVo.FZRDOCTOR = drrBa["FZRDOCTOR"].ToString();
                                    upVo.fpVo.FZZDOCTBH = drrBa["FZZDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZZDOCTBH))
                                        upVo.fpVo.FZZDOCTBH = "-";
                                    upVo.fpVo.FZZDOCT = drrBa["FZZDOCT"].ToString();
                                    upVo.fpVo.FZYDOCTBH = drrBa["FZYDOCTBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FZYDOCTBH))
                                        upVo.fpVo.FZYDOCTBH = "-";
                                    upVo.fpVo.FZYDOCT = drrBa["FZYDOCT"].ToString();
                                    upVo.fpVo.FNURSEBH = drrBa["FNURSEBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FNURSEBH))
                                        upVo.fpVo.FNURSEBH = "-";
                                    upVo.fpVo.FNURSE = drrBa["FNURSE"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FNURSE))
                                        upVo.fpVo.FNURSE = "-";
                                    upVo.fpVo.FJXDOCTBH = drrBa["FJXDOCTBH"].ToString();
                                    upVo.fpVo.FJXDOCT = drrBa["FJXDOCT"].ToString();
                                    upVo.fpVo.FSXDOCTBH = drrBa["FSXDOCTBH"].ToString();
                                    upVo.fpVo.FSXDOCT = drrBa["FSXDOCT"].ToString();
                                    upVo.fpVo.FBMYBH = drrBa["FBMYBH"].ToString();
                                    upVo.fpVo.FBMY = drrBa["FBMY"].ToString();
                                    upVo.fpVo.FQUALITYBH = drrBa["FQUALITYBH"].ToString();
                                    upVo.fpVo.FQUALITY = drrBa["FQUALITY"].ToString();
                                    upVo.fpVo.FZKDOCTBH = drrBa["FZKDOCTBH"].ToString();
                                    if (upVo.fpVo.FZKDOCTBH == "")
                                        upVo.fpVo.FZKDOCTBH = "-";
                                    upVo.fpVo.FZKDOCT = drrBa["FZKDOCT"].ToString();
                                    upVo.fpVo.FZKNURSEBH = drrBa["FZKNURSEBH"].ToString().Trim();
                                    if (upVo.fpVo.FZKNURSEBH == "")
                                        upVo.fpVo.FZKNURSEBH = "-";
                                    upVo.fpVo.FZKNURSE = drrBa["FZKNURSE"].ToString();
                                    if (upVo.fpVo.FZKNURSE == "")
                                        upVo.fpVo.FZKNURSE = "-";
                                    upVo.fpVo.FZKRQ = Function.Datetime(drrBa["FZKRQ"]).ToString("yyyyMMdd");

                                    upVo.fpVo.FLYFSBH = drrBa["FLYFSBH"].ToString().Trim();
                                    if (upVo.fpVo.FLYFSBH != "1" || upVo.fpVo.FLYFSBH != "2" ||
                                        upVo.fpVo.FLYFSBH != "3" || upVo.fpVo.FLYFSBH != "4" || upVo.fpVo.FLYFSBH != "5")
                                        upVo.fpVo.FLYFSBH = "9";

                                    upVo.fpVo.FLYFS = drrBa["FLYFS"].ToString();
                                    if (upVo.fpVo.FLYFS.Length >= 26)
                                        upVo.fpVo.FLYFS = upVo.fpVo.FLYFS.Substring(0, 50);

                                    upVo.fpVo.FYZOUTHOSTITAL = drrBa["FYZOUTHOSTITAL"].ToString();
                                    upVo.fpVo.FSQOUTHOSTITAL = drrBa["FSQOUTHOSTITAL"].ToString();
                                    upVo.fpVo.FISAGAINRYBH = drrBa["FISAGAINRYBH"].ToString();
                                    if (upVo.fpVo.FISAGAINRYBH == "")
                                        upVo.fpVo.FISAGAINRYBH = "-";
                                    upVo.fpVo.FISAGAINRY = drrBa["FISAGAINRY"].ToString();
                                    if (upVo.fpVo.FISAGAINRY == "")
                                        upVo.fpVo.FISAGAINRY = "-";
                                    upVo.fpVo.FISAGAINRYMD = drrBa["FISAGAINRYMD"].ToString();
                                    if (upVo.fpVo.FISAGAINRYMD == "")
                                        upVo.fpVo.FISAGAINRYMD = "-";
                                    upVo.fpVo.FRYQHMDAYS = drrBa["FRYQHMDAYS"].ToString();
                                    upVo.fpVo.FRYQHMHOURS = drrBa["FRYQHMHOURS"].ToString();
                                    upVo.fpVo.FRYQHMMINS = drrBa["FRYQHMMINS"].ToString();
                                    upVo.fpVo.FRYQHMCOUNTS = drrBa["FRYQHMCOUNTS"].ToString();
                                    upVo.fpVo.FRYHMDAYS = drrBa["FRYHMDAYS"].ToString();
                                    upVo.fpVo.FRYHMHOURS = drrBa["FRYHMHOURS"].ToString();
                                    upVo.fpVo.FRYHMMINS = drrBa["FRYHMMINS"].ToString();
                                    upVo.fpVo.FRYHMCOUNTS = drrBa["FRYHMCOUNTS"].ToString();
                                    upVo.fpVo.FSUM1 = Function.Dec(drrBa["FSUM1"].ToString());
                                    upVo.fpVo.FZFJE = Function.Dec(drrBa["FZFJE"].ToString());
                                    upVo.fpVo.FZHFWLYLF = Function.Dec(drrBa["FZHFWLYLF"].ToString());
                                    upVo.fpVo.FZHFWLCZF = Function.Dec(drrBa["FZHFWLCZF"].ToString());
                                    upVo.fpVo.FZHFWLHLF = Function.Dec(drrBa["FZHFWLHLF"].ToString());
                                    upVo.fpVo.FZHFWLQTF = Function.Dec(drrBa["FZHFWLQTF"].ToString());
                                    upVo.fpVo.FZDLBLF = Function.Dec(drrBa["FZDLBLF"].ToString());
                                    upVo.fpVo.FZDLSSSF = Function.Dec(drrBa["FZDLSSSF"].ToString());
                                    upVo.fpVo.FZDLYXF = Function.Dec(drrBa["FZDLYXF"].ToString());
                                    upVo.fpVo.FZDLLCF = Function.Dec(drrBa["FZDLLCF"].ToString());
                                    upVo.fpVo.FZLLFFSSF = Function.Dec(drrBa["FZLLFFSSF"].ToString());
                                    upVo.fpVo.FZLLFWLZWLF = Function.Dec(drrBa["FZLLFWLZWLF"].ToString());
                                    upVo.fpVo.FZLLFSSF = Function.Dec(drrBa["FZLLFSSF"].ToString());
                                    upVo.fpVo.FZLLFMZF = Function.Dec(drrBa["FZLLFMZF"].ToString());
                                    upVo.fpVo.FZLLFSSZLF = Function.Dec(drrBa["FZLLFSSZLF"].ToString());
                                    upVo.fpVo.FKFLKFF = Function.Dec(drrBa["FKFLKFF"].ToString());
                                    upVo.fpVo.FZYLZF = Function.Dec(drrBa["FZYLZF"].ToString());
                                    upVo.fpVo.FXYF = Function.Dec(drrBa["FXYF"].ToString());
                                    upVo.fpVo.FXYLGJF = Function.Dec(drrBa["FXYLGJF"].ToString());
                                    upVo.fpVo.FZCHYF = Function.Dec(drrBa["FZCHYF"].ToString());
                                    upVo.fpVo.FZCYF = Function.Dec(drrBa["FZCYF"].ToString());
                                    upVo.fpVo.FXYLXF = Function.Dec(drrBa["FXYLXF"].ToString());
                                    upVo.fpVo.FXYLBQBF = Function.Dec(drrBa["FXYLBQBF"].ToString());
                                    upVo.fpVo.FXYLQDBF = Function.Dec(drrBa["FXYLQDBF"].ToString());
                                    upVo.fpVo.FXYLYXYZF = Function.Dec(drrBa["FXYLYXYZF"].ToString());
                                    upVo.fpVo.FXYLXBYZF = Function.Dec(drrBa["FXYLXBYZF"].ToString());
                                    upVo.fpVo.FHCLCJF = Function.Dec(drrBa["FHCLCJF"].ToString());
                                    upVo.fpVo.FHCLZLF = Function.Dec(drrBa["FHCLZLF"].ToString());
                                    upVo.fpVo.FHCLSSF = Function.Dec(drrBa["FHCLSSF"].ToString());
                                    upVo.fpVo.FQTF = Function.Dec(drrBa["FQTF"]);
                                    upVo.fpVo.FBGLX = drrBa["FBGLX"].ToString();

                                    if (drrBa["fidcard"].ToString() != "")
                                        upVo.fpVo.GMSFHM = drrBa["fidcard"].ToString();
                                    else
                                        upVo.fpVo.GMSFHM = drReg["idcard_chr"].ToString();

                                    upVo.fpVo.FZYF = Function.Dec(drrBa["FZYF"].ToString());
                                    if (drrBa["FZKDATE"] != DBNull.Value)
                                        upVo.fpVo.FZKDATE = Function.Datetime(drrBa["FZKDATE"]).ToString("yyyy-MM-dd");
                                    else
                                        upVo.fpVo.FZKDATE = "";

                                    upVo.fpVo.FZKTIME = Function.Datetime(upVo.fpVo.FZKDATE + " " + upVo.fpVo.FZKTIME).ToString("yyyyMMddHHmmss");
                                    upVo.fpVo.FJOBBH = drrBa["FJOBBH"].ToString();
                                    if (string.IsNullOrEmpty(upVo.fpVo.FJOBBH))
                                        upVo.fpVo.FJOBBH = "90";
                                    upVo.fpVo.FZHFWLYLF01 = Function.Dec(drrBa["FZHFWLYLF01"]);
                                    upVo.fpVo.FZHFWLYLF02 = Function.Dec(drrBa["FZHFWLYLF02"]);
                                    upVo.fpVo.FZYLZDF = Function.Dec(drrBa["FZYLZDF"]);
                                    upVo.fpVo.FZYLZLF = Function.Dec(drrBa["FZYLZLF"]);
                                    upVo.fpVo.FZYLZLF01 = Function.Dec(drrBa["FZYLZLF01"]);
                                    upVo.fpVo.FZYLZLF02 = Function.Dec(drrBa["FZYLZLF02"]);
                                    upVo.fpVo.FZYLZLF03 = Function.Dec(drrBa["FZYLZLF03"]);
                                    upVo.fpVo.FZYLZLF04 = Function.Dec(drrBa["FZYLZLF04"]);
                                    upVo.fpVo.FZYLZLF05 = Function.Dec(drrBa["FZYLZLF05"]);
                                    upVo.fpVo.FZYLZLF06 = Function.Dec(drrBa["FZYLZLF06"]);
                                    upVo.fpVo.FZYLQTF = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZCLJGZJF = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYLQTF01 = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYLQTF02 = Function.Dec(drrBa["FZYLQTF"]);
                                    upVo.fpVo.FZYID = drrBa["FZYID"].ToString();

                                    upVo.fpVo.ZYH = ipno;
                                    upVo.fpVo.FPHM = FPHM;
                                    upVo.firstSource = 1;//来源病案
                                    #endregion
                                }
                            }
                        }

                        if (!isExisitBa)
                        {
                            EntityFirstPage firstPageVo = GetPatBaFromJH(registerid);
                            if (firstPageVo != null)
                            {
                                upVo = new EntityPatUpload();
                                upVo.fpVo = new EntityFirstPage();
                                firstPageVo.ZYH = ipno;
                                firstPageVo.FPHM = FPHM;
                                upVo.fpVo = firstPageVo;
                                upVo.fpVo.JZJLH = jzjlh;
                                upVo.fpVo.FWSJGDM = "12441900457226325L";
                                upVo.firstSource = 3;
                            }
                        }

                        if (upVo == null)
                            continue;
                        #endregion

                        #region 出院小结信息
                        upVo.xjVo = GetPatCyxjFromJH(registerid, MZH, upVo.fpVo);
                        #endregion

                        #region  显示列表
                        upVo.XH = ++n;
                        upVo.UPLOADTYPE = 1;
                        upVo.PATNAME = drReg["xm"].ToString();
                        upVo.PATSEX = drReg["sex"].ToString();
                        upVo.IDCARD = drReg["idcard_chr"].ToString();
                        upVo.INPATIENTID = drReg["ipno"].ToString();
                        upVo.INDEPTCODE = drReg["rydeptid"].ToString();
                        upVo.INPATIENTDATE = Function.Datetime(Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd"));
                        upVo.OUTHOSPITALDATE = Function.Datetime(Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd"));
                        upVo.RYSJ = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd HH:mm");
                        upVo.CYSJ = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd HH:mm");
                        upVo.FPRN = upVo.fpVo.FPRN;
                        upVo.FTIMES = drReg["rycs"].ToString();
                        upVo.BIRTH = Function.Datetime(drReg["birth"]).ToString("yyyy-mm-dd");
                        upVo.InDeptName = drReg["ryks"].ToString();
                        upVo.OutDeptName = drReg["cyks"].ToString();
                        upVo.OUTDEPTCODE = drReg["cydeptid"].ToString();
                        upVo.JZJLH = jzjlh;
                        upVo.REGISTERID = drReg["registerid_chr"].ToString();
                        upVo.STATUS = Function.Int(drReg["status"]);
                        if (drReg["status"].ToString() == "1")
                            upVo.SZ = "已上传";
                        else
                            upVo.SZ = "未上传";

                        if (drReg["jbr"] != DBNull.Value)
                            upVo.JBRXM = drReg["jbr"].ToString();
                        if (drReg["uploaddate"] != DBNull.Value)
                        {
                            upVo.UPLOADDATE = Function.Datetime(drReg["uploaddate"]);
                            upVo.uploadDateStr = Function.Datetime(drReg["uploaddate"]).ToString("yyyy-MM-dd HH:mm");
                        }
                        #endregion

                        data.Add(upVo);
                    }
                }
                #endregion

            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatFirstList--" + e);
            }
            finally
            {
                svc = null;
            }
            return data;
        }
        #endregion

        #region 查询对应
        /// <summary>
        /// 查询对应
        /// </summary>
        /// <param name="parmStr"></param>
        /// <returns></returns>
        public void GetQuerypat(string dteBegin, string dteEnd, string parmStr, out List<EntityQueryBa> dataIcare, out List<EntityQueryBa> dataBa)
        {
            string SqlBa = string.Empty;
            string SqlReg = string.Empty;
            string SqlJs = string.Empty;
            IDataParameter[] parm = null;
            SqlHelper svcBa = null;
            SqlHelper svc = null;
            dataIcare = new List<EntityQueryBa>();
            dataBa = new List<EntityQueryBa>();
            try
            {
                #region Sql 首页信息
                svcBa = new SqlHelper(EnumBiz.baDB);
                svc = new SqlHelper(EnumBiz.onlineDB);
                SqlBa = @"select 
                                        a.ftimes as FTIMES,
                                        a.fid,
                                        a.fzyid,
                                        a.fcydate,
                                        '' as JZJLH,
                                        '' as FWSJGDM,
                                        '' as FBGLX,
                                        a.fidcard,
                                        a.FFBBHNEW,a.FFBNEW,
                                        a.FASCARD1,
                                        a.FPRN,
                                        a.FNAME,a.FSEXBH,
                                        a.FSEX,a.FBIRTHDAY,
                                        a.FAGE,a.fcountrybh,
                                        a.fcountry,a.fnationalitybh,
                                        a.fnationality,a.FCSTZ,
                                        a.FRYTZ,a.FBIRTHPLACE,
                                        a.FNATIVE,a.FIDCard,
                                        a.FJOB,a.FSTATUSBH,
                                        a.FSTATUS,a.FCURRADDR,
                                        a.FCURRTELE,a.FCURRPOST,
                                        a.FHKADDR,a.FHKPOST,
                                        a.FDWNAME,a.FDWADDR,
                                        a.FDWTELE,a.FDWPOST,
                                        a.FLXNAME,a.FRELATE,
                                        a.FLXADDR,a.FLXTELE,
                                        a.FRYTJBH,a.FRYTJ,
                                        a.FRYDATE,a.FRYTIME,
                                        a.FRYTYKH,a.FRYDEPT,
                                        a.FRYBS,a.FZKTYKH,
                                        a.FZKDEPT,a.FZKTIME,
                                        a.FCYDATE,a.FCYTIME,
                                        a.FCYTYKH,a.FCYDEPT,
                                        a.FCYBS,a.FDAYS,
                                        a.FMZZDBH,a.FMZZD,
                                        a.FMZDOCTBH,a.FMZDOCT,
                                        a.FJBFXBH,a.FJBFX,
                                        a.FYCLJBH,a.FYCLJ,
                                        a.FQJTIMES,a.FQJSUCTIMES,
                                        a.FPHZD,a.FPHZDNUM,
                                        a.FPHZDBH,a.FIFGMYWBH,
                                        a.FIFGMYW,a.FGMYW,
                                        a.FBODYBH,a.FBODY,
                                        a.FBLOODBH,a.FBLOOD,
                                        a.FRHBH,a.FRH,
                                        a.FKZRBH,a.FKZR,
                                        a.FZRDOCTBH,a.FZRDOCTOR,
                                        a.FZZDOCTBH,a.FZZDOCT,
                                        a.FZYDOCTBH,a.FZYDOCT,
                                        a.FNURSEBH,a.FNURSE,
                                        a.FJXDOCTBH,a.FJXDOCT,
                                        a.FSXDOCTBH,a.FSXDOCT,
                                        a.FBMYBH,
                                        a.FBMY,a.FQUALITYBH,
                                        a.FQUALITY,a.FZKDOCTBH,
                                        a.FZKDOCT,a.FZKNURSEBH,
                                        a.FZKNURSE,a.FZKRQ,
                                        a.FLYFSBH,a.FLYFS,a.FYZOUTHOSTITAL,
                                        a.FSQOUTHOSTITAL,a.FISAGAINRYBH,
                                        a.FISAGAINRY,a.FISAGAINRYMD,
                                        a.FRYQHMDAYS,a.FRYQHMHOURS,
                                        a.FRYQHMMINS,a.FRYQHMCOUNTS,
                                        a.FRYHMDAYS,a.FRYHMHOURS,
                                        a.FRYHMMINS,a.FRYHMCOUNTS,a.FSUM1,
                                        a.FZFJE,a.FZHFWLYLF,a.FZHFWLCZF,a.FZHFWLHLF,
                                        a.FZHFWLQTF,a.FZDLBLF,a.FZDLSSSF,
                                        a.FZDLYXF,a.FZDLLCF,a.FZLLFFSSF,a.FZLLFWLZWLF,
                                        a.FZLLFSSF,a.FZLLFMZF,
                                        a.FZLLFSSZLF,a.FKFLKFF,a.FZYLZF,
                                        a.FXYF,a.FXYLGJF,a.FZCHYF,
                                        a.FZCYF,a.FXYLXF,a.FXYLBQBF,
                                        a.FXYLQDBF,a.FXYLYXYZF,a.FXYLXBYZF,
                                        a.FHCLCJF,a.FHCLZLF,a.FHCLSSF,
                                        a.FQTF,a.FZYF,a.FZKDATE,
                                        a.FJOBBH,a.FZHFWLYLF01,a.FZHFWLYLF02,
                                        a.FZYLZDF,a.FZYLZLF,a.FZYLZLF01,a.FZYLZLF02,
                                        a.FZYLZLF03,a.FZYLZLF04,a.FZYLZLF05,a.FZYLZLF06,a.FZYLQTF,
                                        a.FZCLJGZJF,a.FZYLQTF01,a.FZYLQTF02
                                        from tPatientVisit a where a.fzyid is not null ";
                #endregion

                #region SqlReg  查找住院记录

                SqlReg = @"select t1.registerid_chr,
                                t1.patientid_chr as MZH,
                                d.lastname_vchr as xm,
                                d.birth_dat as birth,
                                d.sex_chr as sex,
                                d.idcard_chr,
                                d.homeaddress_vchr as YJDZ,
                                t1.inpatientid_chr as ipno,
                                t1.inpatientcount_int as rycs,
                                t1.deptid_chr as rydeptid,
                                t11.deptname_vchr as ryks,
                                c.outdeptid_chr as cydeptid,
                                c1.deptname_vchr as cyks,
                                to_char(t1.inpatient_dat, 'yyyymmdd') as RYRQ1,
                                to_char(c.outhospital_dat, 'yyyymmdd') as CYRQ1,
                                t1.inpatient_dat as RYSJ,
                                c.modify_dat as CYSJ,
                                rehis.emrinpatientid,
                                rehis.emrinpatientdate,
                                ee.lastname_vchr as jbr,
                                --dd.serno,
                                dd.status,
                                dd.uploaddate
                                from t_opr_bih_register t1
                                left join t_bse_deptdesc t11
                                on t1.deptid_chr = t11.deptid_chr
                                left join t_opr_bih_leave c
                                on t1.registerid_chr = c.registerid_chr
                                left join t_bse_deptdesc c1
                                on c.outdeptid_chr = c1.deptid_chr
                                left join t_opr_bih_registerdetail d
                                on t1.registerid_chr = d.registerid_chr
                                left join t_bse_hisemr_relation rehis
                                on t1.registerid_chr = rehis.registerid_chr
                                left join t_upload dd
                                on t1.registerid_chr = dd.registerid
                                left join t_bse_employee ee
                                on dd.opercode = ee.empno_chr
                                where c.status_int = 1 ";
                #endregion

                #region 结算记录
                SqlJs = @"select a.registerid_chr, a.jzjlh, a.invoiceno_vchr, b.inpatientid_chr
                                  from t_ins_chargezy_csyb a
                                  left join t_opr_bih_register b
                                    on a.registerid_chr = b.registerid_chr
                                    inner join t_upload c
                                        on a.registerid_chr = c.registerid 
                                 where (a.createtime between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))  ";


                #endregion

                #region 条件
                string strSubJs = string.Empty;
                strSubJs = "and (a.jzjlh = '" + parmStr + "' or b.inpatientid_chr = '" + parmStr + "')";
                #endregion

                #region 赋值

                if (!string.IsNullOrEmpty(strSubJs))
                    SqlJs += strSubJs;

                parm = svc.CreateParm(2);
                parm[0].Value = dteBegin + " 00:00:00";
                parm[1].Value = dteEnd + " 23:59:59";
                DataTable dtJs = svc.GetDataTable(SqlJs, parm);

                #region
                if (dtJs != null && dtJs.Rows.Count > 0)
                {
                    string ipnoStr = string.Empty;
                    string registeridStr = string.Empty;
                    List<string> lstReg = new List<string>();
                    List<string> lstIpno = new List<string>();
                    DataTable dtBa = null;
                    DataTable dtReg = null;
                    foreach (DataRow drJs in dtJs.Rows)
                    {
                        string registerid = drJs["registerid_chr"].ToString();
                        string ipno = drJs["inpatientid_chr"].ToString();
                        if (lstReg.Contains(registerid))
                            continue;
                        lstReg.Add(registerid);
                        registeridStr += "'" + registerid + "',";

                        if (lstIpno.Contains(ipno))
                            continue;
                        ipnoStr += "'" + ipno + "',";
                        lstIpno.Add(ipno);
                    }

                    if (!string.IsNullOrEmpty(ipnoStr))
                    {
                        ipnoStr = ipnoStr.TrimEnd(',');
                        registeridStr = registeridStr.TrimEnd(',');
                        SqlBa += " and (a.fprn in (" + ipnoStr + ")" + " or a.fzyid in (" + ipnoStr + ")" + ")";
                        dtBa = svcBa.GetDataTable(SqlBa);

                        SqlReg += "and t1.registerid_chr in (" + registeridStr + ")";
                        dtReg = svc.GetDataTable(SqlReg);
                    }

                    foreach (DataRow drReg in dtReg.Rows)
                    {
                        string MZH = drReg["MZH"].ToString();
                        string emrinpatientid = drReg["emrinpatientid"].ToString();
                        string emrinpatientdate = Function.Datetime(drReg["emrinpatientdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                        string ipno = drReg["ipno"].ToString();
                        string registerid = drReg["registerid_chr"].ToString();
                        int rycs = Function.Int(drReg["rycs"].ToString());
                        string cydate = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd");
                        string cydate1 = Function.Datetime(drReg["cysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string cydate2 = Function.Datetime(drReg["cysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string rydate = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd");
                        string rydate1 = Function.Datetime(drReg["rysj"]).AddDays(-1).ToString("yyyy-MM-dd");
                        string rydate2 = Function.Datetime(drReg["rysj"]).AddDays(1).ToString("yyyy-MM-dd");
                        string jzjlh = string.Empty;
                        string FPHM = string.Empty;

                        #region 查找发票号
                        DataRow[] drrFPHM = dtJs.Select("registerid_chr = '" + registerid + "'");
                        if (drrFPHM.Length > 0)
                        {
                            jzjlh = drrFPHM[0]["jzjlh"].ToString();
                            foreach (DataRow drrF in drrFPHM)
                            {
                                FPHM += drrF["invoiceno_vchr"].ToString() + ",";
                            }
                            if (!string.IsNullOrEmpty(FPHM))
                            {
                                FPHM = FPHM.TrimEnd(',');
                            }
                        }
                        #endregion

                        #region ba
                        DataRow[] drr = dtBa.Select("fprn =  '" + ipno + "' or fzyid = '" + ipno + "'");
                        if (drr.Length > 0)
                        {
                            foreach (DataRow drrBa in drr)
                            {
                                string fcydate = Function.Datetime(drrBa["fcydate"]).ToString("yyyy-MM-dd");
                                string frydate = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                int ftimes = Function.Int(drrBa["FTIMES"].ToString());
                                EntityQueryBa vo = new EntityQueryBa();
                                vo.inpatientId = drrBa["fzyid"].ToString();
                                vo.fprn = drrBa["FPRN"].ToString();
                                vo.name = drrBa["FNAME"].ToString();
                                vo.sex = drrBa["FSEX"].ToString();
                                vo.IDcard = drrBa["FASCARD1"].ToString();
                                vo.inTimes = drrBa["FTIMES"].ToString();
                                vo.rysj = Function.Datetime(drrBa["FRYDATE"]).ToString("yyyy-MM-dd");
                                vo.cysj = Function.Datetime(drrBa["FCYDATE"]).ToString("yyyy-MM-dd");
                                dataBa.Add(vo);
                            }
                        }
                        #endregion

                        #region  显示列表
                        EntityQueryBa voR = new EntityQueryBa();
                        voR.inpatientId = drReg["ipno"].ToString();
                        voR.jzjlh = jzjlh;
                        //voR.fprn = drReg["FPRN"].ToString();
                        voR.name = drReg["xm"].ToString();
                        voR.sex = drReg["sex"].ToString();
                        voR.IDcard = drReg["idcard_chr"].ToString();
                        voR.inTimes = drReg["rycs"].ToString();
                        voR.rysj = Function.Datetime(drReg["rysj"]).ToString("yyyy-MM-dd");
                        voR.cysj = Function.Datetime(drReg["cysj"]).ToString("yyyy-MM-dd");
                        dataIcare.Add(voR);
                        #endregion

                    }
                }
                #endregion

                #endregion
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetQuerypat--" + e);
            }
            finally
            {
                svc = null;
            }
        }
        #endregion

        #region 首页 嘉禾
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipno"></param>
        /// <param name="registerid"></param>
        /// <param name="emrinpatientdate"></param>
        /// <returns></returns>
        public EntityFirstPage GetPatBaFromJH(string registerid)
        {
            EntityFirstPage firstPageVo = null;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.interfaceDB);

                #region
                string sql = @" select  a.PATIENT_ID,
                                        a.ftimes as FTIMES,
                                        a.fid,
                                        a.fzyid,
                                        a.fcydate,
                                        '' as JZJLH,
                                        '' as FWSJGDM,
                                        '' as FBGLX,
                                        a.fidcard,
                                        a.FFBBHNEW,a.FFBNEW,
                                        a.FASCARD1,
                                        a.FPRN,
                                        a.FNAME,a.FSEXBH,
                                        a.FSEX,a.FBIRTHDAY,
                                        a.FAGE,a.fcountrybh,
                                        a.fcountry,a.fnationalitybh,
                                        a.fnationality,a.FCSTZ,
                                        a.FRYTZ,a.FBIRTHPLACE,
                                        a.FNATIVE,a.FIDCard,
                                        a.FJOB,a.FSTATUSBH,
                                        a.FSTATUS,a.FCURRADDR,
                                        a.FCURRTELE,a.FCURRPOST,
                                        a.FHKADDR,a.FHKPOST,
                                        a.FDWNAME,a.FDWADDR,
                                        a.FDWTELE,a.FDWPOST,
                                        a.FLXNAME,a.FRELATE,
                                        a.FLXADDR,a.FLXTELE,
                                        a.FRYTJBH,a.FRYTJ,
                                        a.FRYDATE,a.FRYTIME,
                                        a.FRYTYKH,a.FRYDEPT,
                                        a.FRYBS,a.FZKTYKH,
                                        a.FZKDEPT,a.FZKTIME,
                                        a.FCYDATE,a.FCYTIME,
                                        a.FCYTYKH,a.FCYDEPT,
                                        a.FCYBS,a.FDAYS,
                                        a.FMZZDBH,a.FMZZD,
                                        a.FMZDOCTBH,a.FMZDOCT,
                                        a.FJBFXBH,a.FJBFX,
                                        a.FYCLJBH,a.FYCLJ,
                                        a.FQJTIMES,a.FQJSUCTIMES,
                                        a.FPHZD,a.FPHZDNUM,
                                        a.FPHZDBH,a.FIFGMYWBH,
                                        a.FIFGMYW,a.FGMYW,
                                        a.FBODYBH,a.FBODY,
                                        a.FBLOODBH,a.FBLOOD,
                                        a.FRHBH,a.FRH,
                                        a.FKZRBH,a.FKZR,
                                        a.FZRDOCTBH,a.FZRDOCTOR,
                                        a.FZZDOCTBH,a.FZZDOCT,
                                        a.FZYDOCTBH,a.FZYDOCT,
                                        a.FNURSEBH,a.FNURSE,
                                        a.FJXDOCTBH,a.FJXDOCT,
                                        a.FSXDOCTBH,a.FSXDOCT,
                                        a.FBMYBH,
                                        a.FBMY,a.FQUALITYBH,
                                        a.FQUALITY,a.FZKDOCTBH,
                                        a.FZKDOCT,a.FZKNURSEBH,
                                        a.FZKNURSE,a.FZKRQ,
                                        a.FLYFSBH,a.FLYFS,a.FYZOUTHOSTITAL,
                                        a.FSQOUTHOSTITAL,a.FISAGAINRYBH,
                                        a.FISAGAINRY,a.FISAGAINRYMD,
                                        a.FRYQHMDAYS,a.FRYQHMHOURS,
                                        a.FRYQHMMINS,a.FRYQHMCOUNTS,
                                        a.FRYHMDAYS,a.FRYHMHOURS,
                                        a.FRYHMMINS,a.FRYHMCOUNTS,a.FSUM1,
                                        a.FZFJE,a.FZHFWLYLF,a.FZHFWLCZF,a.FZHFWLHLF,
                                        a.FZHFWLQTF,a.FZDLBLF,a.FZDLSSSF,
                                        a.FZDLYXF,a.FZDLLCF,a.FZLLFFSSF,a.FZLLFWLZWLF,
                                        a.FZLLFSSF,a.FZLLFMZF,
                                        a.FZLLFSSZLF,a.FKFLKFF,a.FZYLZF,
                                        a.FXYF,a.FXYLGJF,a.FZCHYF,
                                        a.FZCYF,a.FXYLXF,a.FXYLBQBF,
                                        a.FXYLQDBF,a.FXYLYXYZF,a.FXYLXBYZF,
                                        a.FHCLCJF,a.FHCLZLF,a.FHCLSSF,
                                        a.FQTF,a.FZYF,a.FZKDATE,
                                        a.FJOBBH,a.FZHFWLYLF01,a.FZHFWLYLF02,
                                        a.FZYLZDF,a.FZYLZLF,a.FZYLZLF01,a.FZYLZLF02,
                                        a.FZYLZLF03,a.FZYLZLF04,a.FZYLZLF05,a.FZYLZLF06,a.FZYLQTF,
                                        a.FZCLJGZJF,a.FZYLQTF01,a.FZYLQTF02
                                        from jhemr.jhemr_his_ba1 a where a.PATIENT_ID in ? ";
                #endregion

                if (string.IsNullOrEmpty(registerid))
                    return null;

                IDataParameter param = svc.CreateParm();
                param.Value = registerid;
                DataTable dt = svc.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    firstPageVo = new EntityFirstPage();
                    DataRow dr = dt.Rows[0];

                    #region 首页信息  
                    firstPageVo.FWSJGDM = dr["FWSJGDM"].ToString();
                    firstPageVo.FFBBHNEW = dr["FFBBHNEW"].ToString();
                    firstPageVo.FFBNEW = dr["FFBNEW"].ToString();
                    if (dr["FASCARD1"] != DBNull.Value)
                        firstPageVo.FASCARD1 = dr["FASCARD1"].ToString();
                    else
                        firstPageVo.FASCARD1 = "1";
                    firstPageVo.FTIMES = Function.Int(dr["FTIMES"].ToString());
                    firstPageVo.FPRN = dr["FPRN"].ToString();
                    firstPageVo.FNAME = dr["FNAME"].ToString();
                    firstPageVo.FSEXBH = dr["FSEXBH"].ToString();
                    firstPageVo.FSEX = dr["FSEX"].ToString();
                    firstPageVo.FBIRTHDAY = Function.Datetime(dr["FBIRTHDAY"]).ToString("yyyyMMdd");
                    firstPageVo.FAGE = dr["FAGE"].ToString();
                    firstPageVo.fcountrybh = dr["fcountrybh"].ToString();
                    if (firstPageVo.fcountrybh == "")
                        firstPageVo.fcountrybh = "-";
                    firstPageVo.fcountry = dr["fcountry"].ToString();
                    if (firstPageVo.fcountry == "")
                        firstPageVo.fcountry = "-";
                    firstPageVo.fnationalitybh = dr["fnationalitybh"].ToString();
                    if (firstPageVo.fnationalitybh == "")
                        firstPageVo.fnationalitybh = "-";
                    firstPageVo.fnationality = dr["fnationality"].ToString();
                    firstPageVo.FCSTZ = dr["FCSTZ"].ToString();
                    firstPageVo.FRYTZ = dr["FRYTZ"].ToString();
                    firstPageVo.FBIRTHPLACE = dr["FBIRTHPLACE"].ToString();
                    firstPageVo.FNATIVE = dr["FNATIVE"].ToString();
                    firstPageVo.FIDCard = dr["FIDCard"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FIDCard))
                        firstPageVo.FIDCard = "无";
                    firstPageVo.FJOB = dr["FJOB"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FJOB))
                        firstPageVo.FJOB = "其他";
                    firstPageVo.FSTATUS = dr["FSTATUS"].ToString().Trim();
                    if (firstPageVo.FSTATUS.Contains("已婚"))
                        firstPageVo.FSTATUSBH = "2";
                    else if (firstPageVo.FSTATUS == "未婚")
                        firstPageVo.FSTATUSBH = "1";
                    else if (firstPageVo.FSTATUS == "丧偶")
                        firstPageVo.FSTATUSBH = "3";
                    else if (firstPageVo.FSTATUS == "离婚")
                        firstPageVo.FSTATUSBH = "4";
                    else
                        firstPageVo.FSTATUSBH = "9";
                    firstPageVo.FCURRADDR = dr["FCURRADDR"].ToString();
                    firstPageVo.FCURRTELE = dr["FCURRTELE"].ToString();
                    firstPageVo.FCURRPOST = dr["FCURRPOST"].ToString();
                    firstPageVo.FHKADDR = dr["FHKADDR"].ToString();
                    firstPageVo.FHKPOST = dr["FHKPOST"].ToString();
                    firstPageVo.FDWNAME = dr["FDWNAME"].ToString();
                    firstPageVo.FDWADDR = dr["FDWADDR"].ToString();
                    firstPageVo.FDWTELE = dr["FDWTELE"].ToString();
                    firstPageVo.FDWPOST = dr["FDWPOST"].ToString();
                    firstPageVo.FLXNAME = dr["FLXNAME"].ToString();
                    firstPageVo.FRELATE = dr["FRELATE"].ToString();
                    if (firstPageVo.FRELATE.Length > 10)
                        firstPageVo.FRELATE = firstPageVo.FRELATE.Substring(0, 10);
                    firstPageVo.FLXADDR = dr["FLXADDR"].ToString();
                    firstPageVo.FLXTELE = dr["FLXTELE"].ToString();
                    firstPageVo.FRYTJBH = dr["FRYTJBH"].ToString();
                    if (firstPageVo.FRYTJBH == "")
                        firstPageVo.FRYTJBH = "-";
                    firstPageVo.FRYTJ = dr["FRYTJ"].ToString();
                    if (firstPageVo.FRYTJ == "")
                        firstPageVo.FRYTJ = "-";
                    firstPageVo.FRYDATE = Function.Datetime(dr["FRYDATE"]).ToString("yyyy-MM-dd");
                    firstPageVo.FRYTIME = dr["FRYTIME"].ToString();
                    firstPageVo.FRYTIME = Function.Datetime(dr["FRYTIME"]).ToString("HH:mm:ss");
                    firstPageVo.FRYTYKH = dr["FRYTYKH"].ToString();
                    firstPageVo.FRYDEPT = dr["FRYDEPT"].ToString();
                    firstPageVo.FRYBS = dr["FRYBS"].ToString().Trim();
                    if (firstPageVo.FRYBS == "")
                        firstPageVo.FRYBS = firstPageVo.FRYDEPT;
                    firstPageVo.FZKTYKH = dr["FZKTYKH"].ToString();
                    firstPageVo.FZKDEPT = dr["FZKDEPT"].ToString();
                    firstPageVo.FZKTIME = dr["FZKTIME"].ToString();
                    if (firstPageVo.FZKTIME.Length < 4)
                        firstPageVo.FZKTIME = Function.Datetime(dr["FZKTIME"]).ToString("HH:mm:ss");
                    firstPageVo.FCYDATE = Function.Datetime(dr["FCYDATE"]).ToString("yyyy-MM-dd");

                    firstPageVo.FCYTIME = dr["FCYTIME"].ToString();
                    if (firstPageVo.FCYTIME.Length < 4)
                        firstPageVo.FCYTIME = Function.Datetime(dr["FCYTIME"]).ToString("HH:mm:ss");
                    firstPageVo.FCYTYKH = dr["FCYTYKH"].ToString();
                    firstPageVo.FCYDEPT = dr["FCYDEPT"].ToString();
                    firstPageVo.FCYBS = dr["FCYBS"].ToString().Trim();
                    if (firstPageVo.FCYBS == "")
                        firstPageVo.FCYBS = firstPageVo.FCYDEPT;
                    TimeSpan ts = Function.Datetime(firstPageVo.FCYDATE) - Function.Datetime(firstPageVo.FRYDATE);
                    firstPageVo.FDAYS = ts.Days.ToString();
                    if (firstPageVo.FDAYS == "0")
                        firstPageVo.FDAYS = "1";

                    firstPageVo.FMZZDBH = dr["FMZZDBH"].ToString();
                    firstPageVo.FMZZD = dr["FMZZD"].ToString();
                    firstPageVo.FMZDOCTBH = dr["FMZDOCTBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FMZDOCTBH))
                        firstPageVo.FMZDOCTBH = "无";
                    firstPageVo.FMZDOCT = dr["FMZDOCT"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FMZDOCT))
                        firstPageVo.FMZDOCT = "无";
                    firstPageVo.FJBFXBH = dr["FJBFXBH"].ToString();
                    firstPageVo.FJBFX = dr["FJBFX"].ToString();
                    firstPageVo.FYCLJBH = dr["FYCLJBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FYCLJBH))
                        firstPageVo.FYCLJBH = "2";
                    firstPageVo.FYCLJ = dr["FYCLJ"].ToString();
                    if (!string.IsNullOrEmpty(firstPageVo.FYCLJBH))
                        firstPageVo.FYCLJ = "是";
                    else
                        firstPageVo.FYCLJ = "否";
                    firstPageVo.FQJTIMES = dr["FQJTIMES"].ToString();
                    firstPageVo.FQJSUCTIMES = dr["FQJSUCTIMES"].ToString();
                    if (!string.IsNullOrEmpty(firstPageVo.FQJTIMES) && string.IsNullOrEmpty(firstPageVo.FQJSUCTIMES))
                    {
                        firstPageVo.FQJSUCTIMES = firstPageVo.FQJTIMES;
                    }
                    firstPageVo.FPHZD = dr["FPHZD"].ToString();
                    if (firstPageVo.FPHZD.Length > 100)
                        firstPageVo.FPHZD = firstPageVo.FPHZD.Substring(0, 100);

                    if (dr["FPHZDNUM"].ToString().Trim() != "")
                        firstPageVo.FPHZDNUM = dr["FPHZDNUM"].ToString();
                    else
                        firstPageVo.FPHZDNUM = "-";

                    if (dr["FPHZDBH"].ToString().Trim() != "")
                        firstPageVo.FPHZDBH = dr["FPHZDBH"].ToString();
                    else
                        firstPageVo.FPHZDBH = "0";

                    firstPageVo.FIFGMYWBH = dr["FIFGMYWBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FIFGMYWBH))
                        firstPageVo.FIFGMYWBH = "1";
                    if (dr["FIFGMYW"].ToString() != "")
                        firstPageVo.FIFGMYW = dr["FIFGMYW"].ToString();
                    else
                        firstPageVo.FIFGMYW = "-";
                    if (dr["FGMYW"].ToString() != "")
                        firstPageVo.FGMYW = dr["FGMYW"].ToString();
                    else
                        firstPageVo.FGMYW = "-";
                    if (dr["FBODYBH"].ToString().Trim() != "")
                        firstPageVo.FBODYBH = dr["FBODYBH"].ToString();
                    else
                        firstPageVo.FBODYBH = "2";
                    if (dr["FBODY"].ToString().Trim() != "")
                        firstPageVo.FBODY = dr["FBODY"].ToString();
                    else
                        firstPageVo.FBODY = "否";
                    firstPageVo.FBLOODBH = dr["FBLOODBH"].ToString();
                    firstPageVo.FBLOOD = dr["FBLOOD"].ToString();
                    firstPageVo.FRHBH = dr["FRHBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FRHBH))
                        firstPageVo.FRHBH = "4";
                    firstPageVo.FRH = dr["FRH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FRH))
                        firstPageVo.FRH = "未查";
                    firstPageVo.FKZRBH = dr["FKZRBH"].ToString();
                    firstPageVo.FKZR = dr["FKZR"].ToString();
                    firstPageVo.FZRDOCTBH = dr["FZRDOCTBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FZRDOCTBH))
                        firstPageVo.FZRDOCTBH = "-";
                    firstPageVo.FZRDOCTOR = dr["FZRDOCTOR"].ToString();
                    firstPageVo.FZZDOCTBH = dr["FZZDOCTBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FZZDOCTBH))
                        firstPageVo.FZZDOCTBH = "-";
                    firstPageVo.FZZDOCT = dr["FZZDOCT"].ToString();
                    firstPageVo.FZYDOCTBH = dr["FZYDOCTBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FZYDOCTBH))
                        firstPageVo.FZYDOCTBH = "-";
                    firstPageVo.FZYDOCT = dr["FZYDOCT"].ToString();
                    firstPageVo.FNURSEBH = dr["FNURSEBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FNURSEBH))
                        firstPageVo.FNURSEBH = "-";
                    firstPageVo.FNURSE = dr["FNURSE"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FNURSE))
                        firstPageVo.FNURSE = "-";
                    firstPageVo.FJXDOCTBH = dr["FJXDOCTBH"].ToString();
                    firstPageVo.FJXDOCT = dr["FJXDOCT"].ToString();
                    firstPageVo.FSXDOCTBH = dr["FSXDOCTBH"].ToString();
                    firstPageVo.FSXDOCT = dr["FSXDOCT"].ToString();
                    firstPageVo.FBMYBH = dr["FBMYBH"].ToString();
                    firstPageVo.FBMY = dr["FBMY"].ToString();
                    firstPageVo.FQUALITYBH = dr["FQUALITYBH"].ToString();
                    firstPageVo.FQUALITY = dr["FQUALITY"].ToString();
                    firstPageVo.FZKDOCTBH = dr["FZKDOCTBH"].ToString();
                    if (firstPageVo.FZKDOCTBH == "")
                        firstPageVo.FZKDOCTBH = "-";
                    firstPageVo.FZKDOCT = dr["FZKDOCT"].ToString();
                    firstPageVo.FZKNURSEBH = dr["FZKNURSEBH"].ToString().Trim();
                    if (firstPageVo.FZKNURSEBH == "")
                        firstPageVo.FZKNURSEBH = "-";
                    firstPageVo.FZKNURSE = dr["FZKNURSE"].ToString();
                    if (firstPageVo.FZKNURSE == "")
                        firstPageVo.FZKNURSE = "-";
                    firstPageVo.FZKRQ = Function.Datetime(dr["FZKRQ"]).ToString("yyyyMMdd");

                    firstPageVo.FLYFSBH = dr["FLYFSBH"].ToString().Trim();
                    if (firstPageVo.FLYFSBH != "1" || firstPageVo.FLYFSBH != "2" ||
                        firstPageVo.FLYFSBH != "3" || firstPageVo.FLYFSBH != "4" || firstPageVo.FLYFSBH != "5")
                        firstPageVo.FLYFSBH = "9";

                    firstPageVo.FLYFS = dr["FLYFS"].ToString();
                    if (firstPageVo.FLYFS.Length >= 26)
                        firstPageVo.FLYFS = firstPageVo.FLYFS.Substring(0, 50);

                    firstPageVo.FYZOUTHOSTITAL = dr["FYZOUTHOSTITAL"].ToString();
                    firstPageVo.FSQOUTHOSTITAL = dr["FSQOUTHOSTITAL"].ToString();
                    firstPageVo.FISAGAINRYBH = dr["FISAGAINRYBH"].ToString();
                    if (firstPageVo.FISAGAINRYBH == "")
                        firstPageVo.FISAGAINRYBH = "-";
                    firstPageVo.FISAGAINRY = dr["FISAGAINRY"].ToString();
                    if (firstPageVo.FISAGAINRY == "")
                        firstPageVo.FISAGAINRY = "-";
                    firstPageVo.FISAGAINRYMD = dr["FISAGAINRYMD"].ToString();
                    if (firstPageVo.FISAGAINRYMD == "")
                        firstPageVo.FISAGAINRYMD = "-";
                    firstPageVo.FRYQHMDAYS = dr["FRYQHMDAYS"].ToString();
                    firstPageVo.FRYQHMHOURS = dr["FRYQHMHOURS"].ToString();
                    firstPageVo.FRYQHMMINS = dr["FRYQHMMINS"].ToString();
                    firstPageVo.FRYQHMCOUNTS = dr["FRYQHMCOUNTS"].ToString();
                    firstPageVo.FRYHMDAYS = dr["FRYHMDAYS"].ToString();
                    firstPageVo.FRYHMHOURS = dr["FRYHMHOURS"].ToString();
                    firstPageVo.FRYHMMINS = dr["FRYHMMINS"].ToString();
                    firstPageVo.FRYHMCOUNTS = dr["FRYHMCOUNTS"].ToString();
                    firstPageVo.FSUM1 = Function.Dec(dr["FSUM1"].ToString());
                    firstPageVo.FZFJE = Function.Dec(dr["FZFJE"].ToString());
                    firstPageVo.FZHFWLYLF = Function.Dec(dr["FZHFWLYLF"].ToString());
                    firstPageVo.FZHFWLCZF = Function.Dec(dr["FZHFWLCZF"].ToString());
                    firstPageVo.FZHFWLHLF = Function.Dec(dr["FZHFWLHLF"].ToString());
                    firstPageVo.FZHFWLQTF = Function.Dec(dr["FZHFWLQTF"].ToString());
                    firstPageVo.FZDLBLF = Function.Dec(dr["FZDLBLF"].ToString());
                    firstPageVo.FZDLSSSF = Function.Dec(dr["FZDLSSSF"].ToString());
                    firstPageVo.FZDLYXF = Function.Dec(dr["FZDLYXF"].ToString());
                    firstPageVo.FZDLLCF = Function.Dec(dr["FZDLLCF"].ToString());
                    firstPageVo.FZLLFFSSF = Function.Dec(dr["FZLLFFSSF"].ToString());
                    firstPageVo.FZLLFWLZWLF = Function.Dec(dr["FZLLFWLZWLF"].ToString());
                    firstPageVo.FZLLFSSF = Function.Dec(dr["FZLLFSSF"].ToString());
                    firstPageVo.FZLLFMZF = Function.Dec(dr["FZLLFMZF"].ToString());
                    firstPageVo.FZLLFSSZLF = Function.Dec(dr["FZLLFSSZLF"].ToString());
                    firstPageVo.FKFLKFF = Function.Dec(dr["FKFLKFF"].ToString());
                    firstPageVo.FZYLZF = Function.Dec(dr["FZYLZF"].ToString());
                    firstPageVo.FXYF = Function.Dec(dr["FXYF"].ToString());
                    firstPageVo.FXYLGJF = Function.Dec(dr["FXYLGJF"].ToString());
                    firstPageVo.FZCHYF = Function.Dec(dr["FZCHYF"].ToString());
                    firstPageVo.FZCYF = Function.Dec(dr["FZCYF"].ToString());
                    firstPageVo.FXYLXF = Function.Dec(dr["FXYLXF"].ToString());
                    firstPageVo.FXYLBQBF = Function.Dec(dr["FXYLBQBF"].ToString());
                    firstPageVo.FXYLQDBF = Function.Dec(dr["FXYLQDBF"].ToString());
                    firstPageVo.FXYLYXYZF = Function.Dec(dr["FXYLYXYZF"].ToString());
                    firstPageVo.FXYLXBYZF = Function.Dec(dr["FXYLXBYZF"].ToString());
                    firstPageVo.FHCLCJF = Function.Dec(dr["FHCLCJF"].ToString());
                    firstPageVo.FHCLZLF = Function.Dec(dr["FHCLZLF"].ToString());
                    firstPageVo.FHCLSSF = Function.Dec(dr["FHCLSSF"].ToString());
                    firstPageVo.FQTF = Function.Dec(dr["FQTF"]);
                    firstPageVo.FBGLX = dr["FBGLX"].ToString();

                    if (dr["fidcard"].ToString() != "")
                        firstPageVo.GMSFHM = dr["fidcard"].ToString();
                    else
                        firstPageVo.GMSFHM = dr["idcard_chr"].ToString();

                    firstPageVo.FZYF = Function.Dec(dr["FZYF"].ToString());
                    if (dr["FZKDATE"] != DBNull.Value)
                        firstPageVo.FZKDATE = Function.Datetime(dr["FZKDATE"]).ToString("yyyy-MM-dd");
                    else
                        firstPageVo.FZKDATE = "";

                    firstPageVo.FZKTIME = Function.Datetime(firstPageVo.FZKDATE + " " + firstPageVo.FZKTIME).ToString("yyyyMMddHHmmss");
                    firstPageVo.FJOBBH = dr["FJOBBH"].ToString();
                    if (string.IsNullOrEmpty(firstPageVo.FJOBBH))
                        firstPageVo.FJOBBH = "90";
                    firstPageVo.FZHFWLYLF01 = Function.Dec(dr["FZHFWLYLF01"]);
                    firstPageVo.FZHFWLYLF02 = Function.Dec(dr["FZHFWLYLF02"]);
                    firstPageVo.FZYLZDF = Function.Dec(dr["FZYLZDF"]);
                    firstPageVo.FZYLZLF = Function.Dec(dr["FZYLZLF"]);
                    firstPageVo.FZYLZLF01 = Function.Dec(dr["FZYLZLF01"]);
                    firstPageVo.FZYLZLF02 = Function.Dec(dr["FZYLZLF02"]);
                    firstPageVo.FZYLZLF03 = Function.Dec(dr["FZYLZLF03"]);
                    firstPageVo.FZYLZLF04 = Function.Dec(dr["FZYLZLF04"]);
                    firstPageVo.FZYLZLF05 = Function.Dec(dr["FZYLZLF05"]);
                    firstPageVo.FZYLZLF06 = Function.Dec(dr["FZYLZLF06"]);
                    firstPageVo.FZYLQTF = Function.Dec(dr["FZYLQTF"]);
                    firstPageVo.FZCLJGZJF = Function.Dec(dr["FZYLQTF"]);
                    firstPageVo.FZYLQTF01 = Function.Dec(dr["FZYLQTF"]);
                    firstPageVo.FZYLQTF02 = Function.Dec(dr["FZYLQTF"]);
                    firstPageVo.FZYID = dr["FZYID"].ToString();
                    #endregion

                    #region 转科情况（数据集）
                    sql = @"select * from jhemr.jhemr_his_ba2 where Patient_Id = '" + registerid + "'";
                    DataTable dtZk = svc.GetDataTable(sql);
                    if (dtZk != null && dtZk.Rows.Count > 0)
                    {
                        EntityBrzkqk zkVo = null;
                        firstPageVo.lstZkVo = new List<EntityBrzkqk>();

                        foreach (DataRow drZk in dtZk.Rows)
                        {
                            zkVo = new EntityBrzkqk();

                            zkVo.FZKTYKH = drZk["FZKTYKH"].ToString();
                            zkVo.FZKDEPT = drZk["FZKDEPT"].ToString();
                            zkVo.FZKDATE = Function.Datetime(drZk["FZKDATE"]).ToString("yyyy-MM-dd");
                            zkVo.FZKTIME = Function.Datetime(drZk["FZKTIME"].ToString()).ToString("HH:mm:ss");
                            zkVo.FPRN = drZk["FPRN"].ToString();
                            firstPageVo.lstZkVo.Add(zkVo);
                        }
                    }
                    #endregion

                    #region 数据集(病人诊断信息)
                    sql = @"select * from jhemr.jhemr_his_ba3 where Patient_Id = '" + registerid + "'";
                    DataTable dtZd = svc.GetDataTable(sql);
                    if (dtZd != null && dtZd.Rows.Count > 0)
                    {
                        EntityBrzdxx zdVo = null;
                        firstPageVo.lstZdVo = new List<EntityBrzdxx>();

                        foreach (DataRow drZd in dtZd.Rows)
                        {
                            zdVo = new EntityBrzdxx();

                            zdVo.FZDLX = drZd["FZDLX"].ToString();
                            zdVo.FICDVersion = drZd["FICDVersion"].ToString();
                            zdVo.FICDM = drZd["FICDM"].ToString();
                            if (dr["FJBNAME"].ToString().Length > 10)
                                zdVo.FJBNAME = drZd["FJBNAME"].ToString().Substring(0, 10);
                            else
                                zdVo.FJBNAME = drZd["FJBNAME"].ToString();
                            zdVo.FRYBQBH = drZd["FRYBQBH"].ToString();
                            if (zdVo.FRYBQBH == "")
                                zdVo.FRYBQBH = "无";
                            zdVo.FRYBQ = drZd["FRYBQ"].ToString();
                            if (zdVo.FRYBQ == "")
                                zdVo.FRYBQ = "无";
                            zdVo.FPRN = drZd["FPRN"].ToString();
                            firstPageVo.lstZdVo.Add(zdVo);
                        }
                    }
                    #endregion

                    #region 数据集（肿瘤化疗记录）
                    sql = @"select * from jhemr.jhemr_his_ba7 where Patient_Id = '" + registerid + "'";
                    DataTable dtbTumor = svc.GetDataTable(sql);
                    if (dtbTumor != null && dtbTumor.Rows.Count > 0)
                    {
                        firstPageVo.lstHlVo = new List<EntityZlhljlsj>();
                        foreach (DataRow drTemp in dtbTumor.Rows)
                        {
                            EntityZlhljlsj vo = new EntityZlhljlsj();

                            vo.FHLRQ1 = Function.Datetime(drTemp["FHLRQ1"]).ToString("yyyyMMdd");
                            vo.FHLDRUG = drTemp["FHLDRUG"].ToString();
                            vo.FHLPROC = drTemp["FHLPROC"].ToString();
                            vo.FHLLXBH = drTemp["FHLLXBH"].ToString();
                            vo.FHLLX = drTemp["FHLLX"].ToString();
                            vo.FPRN = drTemp["FPRN"].ToString();

                            firstPageVo.lstHlVo.Add(vo);
                        }
                    }

                    #endregion

                    #region 数据集(病人手术信息)
                    sql = @"select * from jhemr.jhemr_his_ba4 where Patient_Id = '" + registerid + "'";
                    DataTable dtFop = svc.GetDataTable(sql);
                    if (dtFop != null && dtFop.Rows.Count > 0)
                    {
                        EntityBrssxx fopVo = null;
                        firstPageVo.lstSsVo = new List<EntityBrssxx>();

                        foreach (DataRow drFop in dtFop.Rows)
                        {
                            fopVo = new EntityBrssxx();
                            fopVo.FNAME = drFop["FNAME"].ToString();
                            if (fopVo.FNAME == "")
                                continue;
                            fopVo.FOPTIMES = drFop["FOPTIMES"].ToString();
                            if (fopVo.FOPTIMES == "0")
                                fopVo.FOPTIMES = "1";
                            fopVo.FOPCODE = drFop["FOPCODE"].ToString();
                            fopVo.FOP = drFop["FOP"].ToString();
                            fopVo.FOPDATE = Function.Datetime(drFop["FOPDATE"]).ToString("yyyyMMdd");
                            fopVo.FQIEKOUBH = drFop["FQIEKOUBH"].ToString() == "" ? "无" : drFop["FQIEKOUBH"].ToString();
                            fopVo.FQIEKOU = drFop["FQIEKOU"].ToString() == "" ? "无" : drFop["FQIEKOU"].ToString();
                            fopVo.FYUHEBH = drFop["FYUHEBH"].ToString() == "" ? "无" : drFop["FYUHEBH"].ToString();
                            if (fopVo.FYUHEBH == "")
                                fopVo.FYUHEBH = "-";
                            fopVo.FYUHE = drFop["FYUHE"].ToString();
                            if (fopVo.FYUHE == "")
                                fopVo.FYUHE = "-";
                            fopVo.FDOCBH = drFop["FDOCBH"].ToString();
                            if (fopVo.FDOCBH == "")
                                fopVo.FDOCBH = "-";
                            fopVo.FDOCNAME = drFop["FDOCNAME"].ToString() == "" ? "无" : drFop["FDOCNAME"].ToString();
                            fopVo.FMAZUIBH = drFop["FMAZUIBH"].ToString();
                            if (fopVo.FMAZUIBH == "")
                                fopVo.FMAZUIBH = "无";
                            if (fopVo.FMZDOCTBH == "")
                                fopVo.FMZDOCTBH = "无";
                            fopVo.FMAZUI = drFop["FMAZUI"].ToString() == "" ? "无" : drFop["FMAZUI"].ToString();
                            fopVo.FIFFSOP = drFop["FIFFSOP"].ToString();
                            if (fopVo.FIFFSOP == "False")
                                fopVo.FIFFSOP = "0";
                            else if (fopVo.FIFFSOP == "True")
                                fopVo.FIFFSOP = "1";
                            fopVo.FOPDOCT1BH = drFop["FOPDOCT1BH"].ToString();
                            if (fopVo.FOPDOCT1BH == "")
                                fopVo.FOPDOCT1BH = "无";
                            fopVo.FOPDOCT1 = drFop["FOPDOCT1"].ToString();
                            if (fopVo.FOPDOCT1 == "")
                                fopVo.FOPDOCT1 = "-";
                            fopVo.FOPDOCT2BH = drFop["FOPDOCT2BH"].ToString();
                            if (fopVo.FOPDOCT2BH == "")
                                fopVo.FOPDOCT2BH = "无";
                            fopVo.FOPDOCT2 = drFop["FOPDOCT2"].ToString();
                            if (fopVo.FOPDOCT2 == "")
                                fopVo.FOPDOCT2 = "无";
                            fopVo.FMZDOCTBH = drFop["FMZDOCTBH"].ToString();
                            if (fopVo.FMZDOCTBH == "")
                                fopVo.FMZDOCTBH = "无";
                            fopVo.FMZDOCT = drFop["FMZDOCT"].ToString();
                            if (fopVo.FMZDOCT == "")
                                fopVo.FMZDOCT = "无";
                            fopVo.FZQSSBH = drFop["FZQSSBH"].ToString();
                            if (fopVo.FZQSSBH == "")
                                fopVo.FZQSSBH = "无";
                            fopVo.FZQSS = drFop["FZQSS"].ToString();
                            fopVo.FSSJBBH = drFop["FSSJBBH"].ToString();
                            if (fopVo.FSSJBBH == "")
                                fopVo.FSSJBBH = "无";
                            fopVo.FSSJB = drFop["FSSJB"].ToString();
                            fopVo.FOPKSNAME = drFop["FOPKSNAME"].ToString();
                            if (fopVo.FOPKSNAME == "")
                                fopVo.FOPKSNAME = "无";
                            fopVo.FOPTYKH = drFop["FOPTYKH"].ToString();
                            if (fopVo.FOPTYKH == "")
                                fopVo.FOPTYKH = "无";

                            fopVo.FPRN = drFop["FPRN"].ToString();
                            firstPageVo.lstSsVo.Add(fopVo);
                        }
                    }
                    #endregion

                    #region 数据集（妇婴卡）
                    sql = @"select * from jhemr.jhemr_his_ba5 where Patient_Id = '" + registerid + "'";
                    DataTable dtFy = svc.GetDataTable(sql);
                    if (dtFy != null && dtFy.Rows.Count > 0)
                    {
                        EntityFyksj fyVo = null;
                        firstPageVo.lstFyVo = new List<EntityFyksj>();

                        foreach (DataRow drFy in dtFy.Rows)
                        {
                            fyVo = new EntityFyksj();

                            fyVo.FBABYNUM = drFy["FBABYNUM"].ToString() == "" ? "-" : drFy["FBABYNUM"].ToString();
                            fyVo.FNAME = drFy["FNAME"].ToString() == "" ? "-" : drFy["FNAME"].ToString();
                            fyVo.FBABYSEXBH = drFy["FBABYSEXBH"].ToString() == "" ? "-" : drFy["FBABYSEXBH"].ToString();
                            fyVo.FBABYSEX = drFy["FBABYSEX"].ToString() == "" ? "-" : drFy["FBABYSEX"].ToString();
                            fyVo.FTZ = drFy["FTZ"].ToString() == "" ? "-" : drFy["FTZ"].ToString();
                            fyVo.FRESULTBH = drFy["FRESULTBH"].ToString() == "" ? "-" : drFy["FRESULTBH"].ToString();
                            fyVo.FRESULT = drFy["FRESULT"].ToString() == "" ? "-" : drFy["FRESULT"].ToString();
                            fyVo.FZGBH = drFy["FZGBH"].ToString() == "" ? "-" : drFy["FZGBH"].ToString();
                            fyVo.FZG = drFy["FZG"].ToString() == "" ? "-" : drFy["FZG"].ToString();
                            fyVo.FBABYSUC = drFy["FBABYSUC"].ToString() == "" ? "0" : drFy["FBABYSUC"].ToString();
                            fyVo.FHXBH = drFy["FHXBH"].ToString() == "" ? "-" : drFy["FHXBH"].ToString();
                            fyVo.FHX = drFy["FHX"].ToString() == "" ? "-" : drFy["FHX"].ToString();
                            fyVo.FPRN = drFy["FPRN"].ToString();
                            firstPageVo.lstFyVo.Add(fyVo);
                        }
                    }
                    #endregion

                    #region 数据集（肿瘤卡）
                    sql = @"select * from jhemr.jhemr_his_ba6 where Patient_Id = '" + registerid + "'";
                    DataTable dtZl = svc.GetDataTable(sql);
                    if (dtZl != null && dtZl.Rows.Count > 0)
                    {
                        EntityZlksj zlVo = null;
                        firstPageVo.lstZlVo = new List<EntityZlksj>();

                        foreach (DataRow drZl in dtZl.Rows)
                        {
                            zlVo = new EntityZlksj();

                            zlVo.FFLFSBH = drZl["FFLFSBH"].ToString();
                            zlVo.FFLFS = drZl["FFLFS"].ToString();
                            zlVo.FFLCXBH = drZl["FFLCXBH"].ToString();
                            zlVo.FFLCX = drZl["FFLCX"].ToString();
                            zlVo.FFLZZBH = drZl["FFLZZBH"].ToString();
                            zlVo.FFLZZ = drZl["FFLZZ"].ToString();
                            zlVo.FYJY = drZl["FYJY"].ToString();
                            zlVo.FYCS = drZl["FYCS"].ToString();
                            zlVo.FYTS = drZl["FYTS"].ToString();
                            zlVo.FYRQ1 = Function.Datetime(drZl["FYRQ1"]).ToString("yyyyMMdd");
                            zlVo.FYRQ2 = Function.Datetime(drZl["FYRQ2"]).ToString("yyyyMMdd");
                            zlVo.FQJY = drZl["FQJY"].ToString();
                            zlVo.FQCS = drZl["FQCS"].ToString();
                            zlVo.FQTS = drZl["FQTS"].ToString();
                            zlVo.FQRQ1 = Function.Datetime(drZl["FQRQ1"]).ToString("yyyyMMdd");
                            zlVo.FQRQ2 = Function.Datetime(drZl["FQRQ2"]).ToString("yyyyMMdd");
                            zlVo.FZNAME = drZl["FZNAME"].ToString();
                            zlVo.FZJY = drZl["FZJY"].ToString();
                            zlVo.FZCS = drZl["FZCS"].ToString();
                            zlVo.FZTS = drZl["FZTS"].ToString();
                            zlVo.FZRQ1 = Function.Datetime(drZl["FZRQ1"]).ToString("yyyyMMdd");
                            zlVo.FZRQ2 = Function.Datetime(drZl["FZRQ2"]).ToString("yyyyMMdd");
                            zlVo.FHLFSBH = drZl["FHLFSBH"].ToString();
                            zlVo.FHLFS = drZl["FHLFS"].ToString();
                            zlVo.FHLFFBH = drZl["FHLFFBH"].ToString();
                            zlVo.FHLFF = drZl["FHLFF"].ToString();
                            zlVo.FPRN = drZl["FPRN"].ToString();

                            if (string.IsNullOrEmpty(zlVo.FFLFSBH) || string.IsNullOrEmpty(zlVo.FHLFSBH))
                                continue;

                            firstPageVo.lstZlVo.Add(zlVo);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }


            return firstPageVo;
        }
        #endregion

        #region 出院小结 嘉禾
        public EntityCyxj GetPatCyxjFromJH(string registerId, string MZH, EntityFirstPage fpVo)
        {
            EntityCyxj xjVo = null;
            SqlHelper svc = null;
            try
            {
                svc = new SqlHelper(EnumBiz.interfaceDB);
                string sql = "select * from jhemr.sp3_3022  a where a.patient_id ='" + registerId + "'";
                DataTable dtXj = svc.GetDataTable(sql);

                #region 上传信息 出院小结

                if (dtXj != null && dtXj.Rows.Count > 0)
                {
                    xjVo = new EntityCyxj();
                    DataRow dr = dtXj.Rows[0];
                    xjVo = new EntityCyxj();
                    xjVo.JZJLH = fpVo.JZJLH;
                    xjVo.MZH = MZH;
                    xjVo.ZYH = dr["ZYH"].ToString();
                    xjVo.MZZD = dr["MZZD"].ToString();
                    if (string.IsNullOrEmpty(xjVo.MZZD))
                        xjVo.MZZD = "-";
                    if (xjVo.MZZD.Length > 100)
                        xjVo.MZZD = xjVo.MZZD.Substring(0, 100);
                    xjVo.RYZD = dr["RYZD"].ToString().Trim();
                    if (string.IsNullOrEmpty(xjVo.RYZD))
                        xjVo.RYZD = fpVo.FMZZD;
                    xjVo.CYZD = dr["CYZD"].ToString().Trim();
                    if (dr["CYZD"] == DBNull.Value)
                        xjVo.CYZD = "-";
                    xjVo.XM = fpVo.FNAME;
                    xjVo.XB = fpVo.FSEX;
                    if (xjVo.XB == "男")
                        xjVo.XB = "1";
                    else if (xjVo.XB == "女")
                        xjVo.XB = "2";
                    else xjVo.XB = "9";
                    xjVo.NL = fpVo.FAGE;
                    if (!string.IsNullOrEmpty(fpVo.FIDCard))
                        xjVo.GMSFHM = fpVo.FIDCard;
                    else
                        xjVo.GMSFHM = dr["GMSFHM"].ToString();
                    xjVo.RYRQ = Function.Datetime(dr["RYRQ"]).ToString("yyyyMMdd");
                    xjVo.CYRQ = Function.Datetime(dr["CYRQ"]).ToString("yyyyMMdd");
                    xjVo.ZYTS = fpVo.FDAYS;
                    xjVo.ZY = fpVo.FJOB;
                    xjVo.JG = fpVo.FNATIVE;
                    if (string.IsNullOrEmpty(xjVo.JG))
                        xjVo.JG = "无";
                    xjVo.YJDZ = dr["YJDZ"].ToString();
                    if (string.IsNullOrEmpty(xjVo.YJDZ))
                        xjVo.YJDZ = "-";
                    xjVo.CYYZ = dr["CYYZ"].ToString().Trim();
                    if (string.IsNullOrEmpty(xjVo.CYYZ))
                        xjVo.CYYZ = "-";
                    xjVo.RYQK = dr["RYQK"].ToString().Trim();
                    if (string.IsNullOrEmpty(xjVo.RYQK))
                        xjVo.RYQK = "-";
                    xjVo.YSQM = dr["YSQM"].ToString().Trim();
                    if (string.IsNullOrEmpty(xjVo.YSQM))
                        xjVo.YSQM = "-";
                    xjVo.RYHCLGC = dr["ZLJG"].ToString().Trim();
                    xjVo.CYSQK = dr["CYSQK"].ToString().Trim();
                    if (string.IsNullOrEmpty(xjVo.CYSQK))
                        xjVo.CYSQK = "-";
                    xjVo.ZLJG = dr["ZLJG"].ToString().Trim();
                    if (xjVo.ZLJG.Length > 1000)
                        xjVo.ZLJG = xjVo.ZLJG.Substring(0, 1000);
                    if (string.IsNullOrEmpty(xjVo.ZLJG))
                        xjVo.ZLJG = "-";
                    if (fpVo.FTIMES > 0)
                        xjVo.FTIMES = fpVo.FTIMES.ToString();

                    xjVo.FSUM1 = Function.Dec(fpVo.FSUM1);
                    xjVo.FPHM = fpVo.FPHM;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }


            return xjVo;
        }
        #endregion

        #region 病案首页 转科信息
        public List<EntityBrzkqk> GetBrzkqk(List<ListViewItem> lstItems, string fprn)
        {
            List<EntityBrzkqk> data = new List<EntityBrzkqk>();

            if (lstItems == null)
            {
                return null;
            }

            for (int iL = 1; iL < lstItems.Count; iL++)
            {
                EntityBrzkqk vo = new EntityBrzkqk();
                ListViewItem item = lstItems[0];
                vo.FPRN = fprn;
                vo.FZKTYKH = item.SubItems[3].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "");
                vo.FZKDEPT = item.SubItems[2].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "");
                vo.FZKDATE = Function.Datetime(item.SubItems[1].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "")).ToString("yyyy-MM-dd");
                vo.FZKTIME = Function.Datetime(item.SubItems[1].ToString().Replace("{ListViewSubItem: {", "").Replace("}}", "")).ToString("HH:mm:ss");
            }

            return data;
        }
        #endregion

        #region 病案首页 诊断信息
        public List<EntityBrzdxx> GetBrzdxx(DataTable dt, DataTable dtbOutDiag, string fprn)
        {
            if (dt == null)
                return null;

            List<EntityBrzdxx> data = new List<EntityBrzdxx>();
            DataRow drDS = dt.Rows[0];
            string outMainDiag = drDS["maindiagnosis"].ToString();
            string outMainDiagICD = drDS["icd_10ofmain"].ToString();
            int FRYBQBH = Function.Int(drDS["mainconditionseq"]);

            EntityBrzdxx voM = new EntityBrzdxx();
            voM.FJBNAME = outMainDiag;
            if (string.IsNullOrEmpty(voM.FJBNAME))
                voM.FJBNAME = "-";
            if (!string.IsNullOrEmpty(outMainDiagICD))
                voM.FICDM = outMainDiagICD;
            else
                voM.FICDM = "-";
            voM.FRYBQBH = (FRYBQBH + 1).ToString();
            if (voM.FRYBQBH == "1")
                voM.FRYBQ = "有";
            else if (voM.FRYBQBH == "2")
                voM.FRYBQ = "临床未确定";
            else if (voM.FRYBQBH == "3")
                voM.FRYBQ = "情况不明";
            else
            {
                voM.FRYBQBH = "4";
                voM.FRYBQ = "无";
            }

            voM.FZDLX = "1";
            voM.FICDVersion = "10";
            voM.FPRN = fprn;
            data.Add(voM);

            string PoisoningReson = drDS["SCACHESOURCE"].ToString().Trim();
            string PoisoningResonICD = drDS["SCACHESOURCEICD"].ToString();
            if (!string.IsNullOrEmpty(PoisoningReson))
            {
                EntityBrzdxx voP = new EntityBrzdxx();
                voP.FJBNAME = PoisoningReson;
                if (string.IsNullOrEmpty(voP.FJBNAME))
                    voP.FJBNAME = "-";
                if (!string.IsNullOrEmpty(PoisoningResonICD))
                    voP.FICDM = PoisoningResonICD;
                else
                    voP.FICDM = "-";
                voP.FRYBQBH = "-";
                voP.FRYBQ = "-";
                voP.FZDLX = "s";
                voP.FICDVersion = "10";
                voP.FPRN = fprn;
                data.Add(voP);
            }

            if (dtbOutDiag != null && dtbOutDiag.Rows.Count > 0)
            {
                foreach (DataRow dr in dtbOutDiag.Rows)
                {
                    EntityBrzdxx vo = new EntityBrzdxx();
                    vo.FPRN = fprn;
                    vo.FZDLX = "2";
                    vo.FICDM = dr["code"].ToString();
                    if (string.IsNullOrEmpty(vo.FICDM))
                        vo.FICDM = "-";
                    vo.FJBNAME = dr["name"].ToString().Trim();
                    vo.FRYBQ = dr["outinfo"].ToString();

                    if (vo.FRYBQ == "有")
                        vo.FRYBQBH = "1";
                    else if (vo.FRYBQ == "临床未确定")
                        vo.FRYBQBH = "2";
                    else if (vo.FRYBQ == "情况不明")
                        vo.FRYBQBH = "3";
                    else if (vo.FRYBQ == "无")
                        vo.FRYBQBH = "4";
                    else
                    {
                        vo.FRYBQBH = "4";
                        vo.FRYBQ = "无";
                    }
                    if (string.IsNullOrEmpty(vo.FJBNAME))
                    {
                        continue;
                    }
                    vo.FICDVersion = "10";

                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 病案首页 肿瘤化疗记录
        public List<EntityZlhljlsj> GetZlhljlsj(DataTable dtbTumor, string fprn)
        {
            if (dtbTumor == null)
                return null;

            List<EntityZlhljlsj> data = new List<EntityZlhljlsj>();

            if (dtbTumor != null && dtbTumor.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtbTumor.Rows)
                {
                    EntityZlhljlsj vo = new EntityZlhljlsj();
                    vo.FHLRQ1 = Function.Datetime(drTemp["curedate"]).ToString("yyyyMMdd");

                    vo.FHLDRUG = drTemp["medicine"].ToString();
                    vo.FHLPROC = drTemp["treatment"].ToString();

                    if (string.IsNullOrEmpty(vo.FHLPROC))
                        vo.FHLPROC = "-";

                    if (drTemp["result"].ToString() == "CR")
                    {
                        vo.FHLLXBH = "1";
                    }
                    else if (drTemp["result"].ToString() == "PR")
                    {
                        vo.FHLLXBH = "2";
                    }
                    else if (drTemp["result"].ToString() == "MR")
                    {
                        vo.FHLLXBH = "3";
                    }
                    else if (drTemp["result"].ToString() == "S")
                    {
                        vo.FHLLXBH = "4";
                    }
                    else if (drTemp["result"].ToString() == "P")
                    {
                        vo.FHLLXBH = "5";
                    }
                    else if (drTemp["result"].ToString() == "NA")
                    {
                        vo.FHLLXBH = "6";
                    }

                    vo.FHLLX = drTemp["result"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }
        #endregion

        #region 病案首页 肿瘤卡
        public List<EntityZlksj> GetZlksj(DataTable dtbTumor, string fprn)
        {
            if (dtbTumor == null)
                return null;

            List<EntityZlksj> data = new List<EntityZlksj>();

            if (dtbTumor != null && dtbTumor.Rows.Count > 0)
            {
                int intTemp = 0;
                foreach (DataRow drInfo in dtbTumor.Rows)
                {
                    EntityZlksj vo = new EntityZlksj();
                    vo.FPRN = fprn;
                    intTemp = Function.Int(drInfo["RTMODESEQ"]);
                    if (intTemp == 0)
                    {
                        vo.FFLFSBH = "1";
                        vo.FFLFS = "根治性";
                    }
                    else if (intTemp == 1)
                    {
                        vo.FFLFSBH = "2";
                        vo.FFLFS = "姑息性";
                    }
                    else if (intTemp == 2)
                    {
                        vo.FFLFSBH = "3";
                        vo.FFLFS = "辅助性";
                    }



                    intTemp = Function.Int(drInfo["RTRULESEQ"]);
                    if (intTemp == 0)
                    {
                        vo.FFLCXBH = "1";
                        vo.FFLFS = "连续";
                    }
                    else if (intTemp == 1)
                    {
                        vo.FFLCXBH = "2";
                        vo.FFLFS = "间断";
                    }
                    else if (intTemp == 2)
                    {
                        vo.FFLCXBH = "3";
                        vo.FFLFS = "分段";
                    }

                    intTemp = Function.Int(drInfo["RTRULESEQ"]);
                    if (intTemp == 0)
                    {
                        vo.FFLCXBH = "1";
                        vo.FFLFS = "连续";
                    }
                    else if (intTemp == 1)
                    {
                        vo.FFLCXBH = "2";
                        vo.FFLFS = "间断";
                    }
                    else if (intTemp == 2)
                    {
                        vo.FFLCXBH = "3";
                        vo.FFLFS = "分段";
                    }

                    if (drInfo["RTCO"].ToString() == "1")
                    {
                        vo.FFLZZBH = "1";
                        vo.FFLZZ = "钴";
                    }
                    else if (drInfo["RTACCELERATOR"].ToString() == "1")
                    {
                        vo.FFLZZBH = "2";
                        vo.FFLZZ = "直加";
                    }
                    else if (drInfo["RTX_RAY"].ToString() == "1")
                    {
                        vo.FFLZZBH = "3";
                        vo.FFLZZ = "X线";
                    }
                    else if (drInfo["RTLACUNA"].ToString() == "1")
                    {
                        vo.FFLZZBH = "4";
                        vo.FFLZZ = "后装";
                    }

                    vo.FYJY = drInfo["ORIGINALDISEASEGY"].ToString();
                    vo.FYCS = drInfo["ORIGINALDISEASETIMES"].ToString();
                    vo.FYTS = drInfo["ORIGINALDISEASEDAYS"].ToString();
                    vo.FYRQ1 = Function.Datetime(drInfo["ORIGINALDISEASEBEGINDATE"]).ToString("yyyyMMdd");
                    vo.FYRQ2 = Function.Datetime(drInfo["ORIGINALDISEASEENDDATE"]).ToString("yyyyMMdd");

                    vo.FQJY = drInfo["LYMPHGY"].ToString();
                    vo.FQCS = drInfo["LYMPHTIMES"].ToString();
                    vo.FQTS = drInfo["LYMPHDAYS"].ToString();
                    vo.FQRQ1 = Function.Datetime(drInfo["LYMPHBEGINDATE"]).ToString("yyyyMMdd");
                    vo.FQRQ2 = Function.Datetime(drInfo["LYMPHENDDATE"]).ToString("yyyyMMdd");

                    vo.FZNAME = "-";
                    vo.FZJY = drInfo["METASTASISGY"].ToString();
                    vo.FZCS = drInfo["METASTASISTIMES"].ToString();
                    vo.FZTS = drInfo["METASTASISDAYS"].ToString();
                    vo.FZRQ1 = Function.Datetime(drInfo["METASTASISBEGINDATE"]).ToString("yyyyMMdd");
                    vo.FZRQ2 = Function.Datetime(drInfo["METASTASISENDDATE"]).ToString("yyyyMMdd");

                    if (drInfo["CHEMOTHERAPYWHOLEBODY"].ToString() == "1")
                    {
                        vo.FHLFSBH = "1";
                        vo.FHLFS = "根治性";
                    }

                    else if (drInfo["CHEMOTHERAPYINTUBATE"].ToString() == "1")
                    {
                        vo.FHLFSBH = "2";
                        vo.FHLFS = "姑息性";
                    }
                    else if (drInfo["CHEMOTHERAPYTHORAX"].ToString() == "1")
                    {
                        vo.FHLFSBH = "3";
                        vo.FHLFS = "新辅助性";
                    }
                    else if (drInfo["CHEMOTHERAPYABDOMEN"].ToString() == "1")
                    {
                        vo.FHLFSBH = "4";
                        vo.FHLFS = "辅助性";
                    }
                    else if (drInfo["CHEMOTHERAPYSPINAL"].ToString() == "1")
                    {
                        vo.FHLFSBH = "5";
                        vo.FHLFS = "新药试用";
                    }
                    else if (drInfo["CHEMOTHERAPYOTHER"].ToString() == "1")
                    {
                        vo.FHLFSBH = "6";
                        vo.FHLFS = "其他";
                    }


                    if (string.IsNullOrEmpty(vo.FHLFSBH) && string.IsNullOrEmpty(vo.FFLFSBH))
                    {
                        continue;
                    }

                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 病案首页 手术信息
        public List<EntityBrssxx> GetBrssxx(DataTable dtbOP, string fprn, string name)
        {
            if (dtbOP == null)
                return null;
            List<EntityBrssxx> data = new List<EntityBrssxx>();

            try
            {
                if (dtbOP != null && dtbOP.Rows.Count > 0)
                {
                    int intOpTimes = 0;
                    foreach (DataRow drTemp in dtbOP.Rows)
                    {
                        EntityBrssxx vo = new EntityBrssxx();
                        vo.FPRN = fprn;
                        vo.FNAME = name;
                        vo.FOPTIMES = (++intOpTimes).ToString();
                        vo.FOPCODE = drTemp["opcode"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPCODE))
                            vo.FOPCODE = "-";
                        vo.FOP = drTemp["opname"].ToString();
                        vo.FOPDATE = Function.Datetime(drTemp["opdate"]).ToString("yyyyMMdd");
                        #region 切口愈合情况
                        string[] strCut = drTemp["cutlevel"].ToString().Split('/');
                        if (strCut != null && strCut.Length == 2)
                        {
                            vo.FQIEKOU = strCut[0];
                            vo.FYUHE = strCut[1];
                            if (strCut[0] == "Ⅰ")
                            {
                                vo.FQIEKOUBH = "1";
                            }
                            else if (strCut[0] == "Ⅱ")
                            {
                                vo.FQIEKOUBH = "2";
                            }
                            else if (strCut[0] == "Ⅲ" || strCut[0] == "III")
                            {
                                vo.FQIEKOUBH = "3";
                            }
                            else
                            {
                                vo.FQIEKOUBH = "1";
                            }

                            if (strCut[1] == "甲")
                            {
                                vo.FYUHEBH = "1";
                            }
                            else if (strCut[1] == "乙")
                            {
                                vo.FYUHEBH = "2";
                            }
                            else if (strCut[1] == "丙")
                            {
                                vo.FYUHEBH = "3";
                            }
                            else
                            {
                                vo.FYUHEBH = "4";
                            }
                        }


                        if (string.IsNullOrEmpty(vo.FQIEKOUBH))
                            vo.FQIEKOUBH = "-";
                        if (string.IsNullOrEmpty(vo.FQIEKOU))
                            vo.FQIEKOU = "-";

                        if (string.IsNullOrEmpty(vo.FYUHEBH))
                            vo.FYUHEBH = "-";
                        if (string.IsNullOrEmpty(vo.FYUHE))
                            vo.FYUHE = "-";
                        #endregion
                        vo.FDOCBH = drTemp["opdoctorno"].ToString();
                        vo.FDOCNAME = drTemp["opdoctor"].ToString();
                        vo.FMAZUIBH = drTemp["anacode"].ToString();
                        if (string.IsNullOrEmpty(vo.FMAZUIBH))
                            vo.FMAZUIBH = "-";
                        vo.FMAZUI = drTemp["Ananame"].ToString();
                        if (string.IsNullOrEmpty(vo.FMAZUI))
                            vo.FMAZUI = "-";
                        if (vo.FMAZUI.Length > 15)
                            vo.FMAZUI = vo.FMAZUI.Substring(0, 15);
                        vo.FIFFSOP = "1";
                        vo.FOPDOCT1BH = drTemp["firstassistno"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT1BH))
                            vo.FOPDOCT1BH = "-";
                        vo.FOPDOCT1 = drTemp["firstassist"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT1))
                            vo.FOPDOCT1 = "-";
                        vo.FOPDOCT2BH = drTemp["secondassistno"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT2BH))
                            vo.FOPDOCT2BH = "-";
                        vo.FOPDOCT2 = drTemp["secondassist"].ToString();
                        if (string.IsNullOrEmpty(vo.FOPDOCT2))
                            vo.FOPDOCT2 = "-";
                        vo.FMZDOCTBH = drTemp["anadoctorno"].ToString();
                        if (string.IsNullOrEmpty(vo.FMZDOCTBH))
                            vo.FMZDOCTBH = "-";
                        vo.FMZDOCT = drTemp["anadoctor"].ToString();
                        if (string.IsNullOrEmpty(vo.FMZDOCT))
                            vo.FMZDOCT = "-";
                        if (drTemp["operationelective"].ToString() == "是")
                        {
                            vo.FZQSSBH = "1";
                            vo.FZQSS = "是";
                        }
                        else
                        {
                            vo.FZQSSBH = "0";
                            vo.FZQSS = "否";
                        }

                        if (drTemp["operationlevel"].ToString() == "一级手术")
                        {
                            vo.FSSJBBH = "1";
                            vo.FSSJB = "一级";
                        }
                        else if (drTemp["operationlevel"].ToString() == "二级手术")
                        {
                            vo.FSSJBBH = "2";
                            vo.FSSJB = "二级";
                        }
                        else if (drTemp["operationlevel"].ToString() == "三级手术")
                        {
                            vo.FSSJBBH = "3";
                            vo.FSSJB = "三级";
                        }
                        else if (drTemp["operationlevel"].ToString() == "四级手术")
                        {
                            vo.FSSJBBH = "4";
                            vo.FSSJB = "四级";
                        }
                        else
                        {
                            vo.FSSJBBH = "1";
                            vo.FSSJB = "一级";
                        }

                        vo.FOPKSNAME = "-";
                        vo.FOPTYKH = "-";
                        data.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return data;
        }
        #endregion

        #region 病案首页 妇婴卡
        public List<EntityFyksj> GetFyksj(DataTable dtbInfant, string fprn, string name)
        {
            if (dtbInfant == null)
                return null;

            List<EntityFyksj> data = new List<EntityFyksj>();
            if (dtbInfant != null && dtbInfant.Rows.Count > 0)
            {
                int iRow = 0;
                foreach (DataRow drTemp in dtbInfant.Rows)
                {
                    EntityFyksj vo = new EntityFyksj();
                    vo.FPRN = fprn;
                    vo.FNAME = name;
                    vo.FBABYNUM = (iRow + 1).ToString();
                    iRow++;
                    if (drTemp["sex"].ToString() == "男")
                    {
                        vo.FBABYSEXBH = "1";
                    }
                    else if (drTemp["sex"].ToString() == "女")
                    {
                        vo.FBABYSEXBH = "2";
                    }
                    vo.FBABYSEX = drTemp["sex"].ToString();
                    vo.FTZ = drTemp["infantweight"].ToString();
                    if (drTemp["LaborResult"].ToString() == "活产")
                    {
                        vo.FRESULTBH = "1";
                    }
                    else if (drTemp["LaborResult"].ToString() == "死产")
                    {
                        vo.FRESULTBH = "2";
                    }
                    else if (drTemp["LaborResult"].ToString() == "死胎")
                    {
                        vo.FRESULTBH = "3";
                    }
                    vo.FRESULT = drTemp["LaborResult"].ToString();
                    if (drTemp["InfantResult"].ToString() == "死亡")
                    {
                        vo.FZGBH = "1";
                    }
                    else if (drTemp["InfantResult"].ToString() == "转科")
                    {
                        vo.FZGBH = "2";
                    }
                    else if (drTemp["InfantResult"].ToString() == "出院")
                    {
                        vo.FZGBH = "3";
                    }
                    vo.FZG = drTemp["InfantResult"].ToString();
                    vo.FBABYSUC = Function.Int(drTemp["rescuesucctimes"]).ToString();
                    if (drTemp["InfantBreath"].ToString() == "自然")
                    {
                        vo.FHXBH = "1";
                    }
                    else if (drTemp["InfantBreath"].ToString() == "Ⅰ度窒息")
                    {
                        vo.FHXBH = "2";
                    }
                    else if (drTemp["InfantBreath"].ToString() == "Ⅱ度窒息")
                    {
                        vo.FHXBH = "3";
                    }
                    vo.FHX = drTemp["InfantBreath"].ToString();
                    data.Add(vo);
                }
            }
            return data;
        }
        #endregion

        #region 获取患者首页其他信息
        /// <summary>
        /// 获取患者首页其他信息
        /// </summary>
        /// <param name="lstUpVo"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetPatFirstInfo(List<EntityPatUpload> lstUpVo)
        {
            string SqlZd = string.Empty;
            string SqlZk = string.Empty;
            string SqlFop = string.Empty;
            string SqlFy = string.Empty;
            string SqlZl = string.Empty;
            string SqlHl = string.Empty;
            string SqlZdfj = string.Empty;
            string SqlCh = string.Empty;

            DataTable DtZd = null;
            DataTable DtZk = null;
            DataTable DtFop = null;
            DataTable DtFy = null;
            DataTable DtZl = null;
            DataTable DtHl = null;
            DataTable DtZdfj = null;
            DataTable DtCh = null;

            SqlHelper svcBa = new SqlHelper(EnumBiz.baDB);

            try
            {
                for (int i = 0; i < lstUpVo.Count; i++)
                {

                    if (lstUpVo[i].firstSource == 2)
                    {
                        continue;
                    }
                    #region 诊断信息
                    SqlZd = @"select b.fid, b.FPRN,b.FTIMES,b.FZDLX,b.FICDVersion,b.FICDM,b.FJBNAME,b.FRYBQBH,b.FRYBQ 
                           from  tDiagnose  b where b.fprn = ? and b.ftimes = ? ";
                    #endregion

                    #region 转科信息
                    SqlZk = @"select * from tSwitchKs a where a.fprn = ? and a.ftimes = ? ";
                    #endregion

                    #region 手术信息
                    SqlFop = @"select FPRN,FTIMES,FNAME,FOPTIMES,FOPCODE,FOP,FOPDATE,FQIEKOUBH,FQIEKOU,FYUHEBH,
                            FYUHE,FDOCBH,FDOCNAME,FMAZUIBH,FMAZUI,FIFFSOP,FOPDOCT1BH,FOPDOCT1,FOPDOCT2BH,
                            FOPDOCT2,FMZDOCTBH,FMZDOCT,FZQSSBH,FZQSS,FSSJBBH,FSSJB,FOPKSNAME,FOPTYKH
                            from tOperation a where a.fprn = ? and a.ftimes = ? ";
                    #endregion

                    #region 妇婴卡信息
                    SqlFy = @"select distinct c.ftimes, c.FPRN,c.FTIMES,c.FBABYNUM,c.FNAME,c.FBABYSEXBH,c.FBABYSEX,c.FTZ,c.FRESULTBH,c.FRESULT,
                            c.FZGBH,c.FZG,c.FBABYSUC,c.FHXBH,c.FHX from  tBabyCard c where c.fprn = ? and c.ftimes = ?";
                    #endregion

                    #region 肿瘤卡信息
                    SqlZl = @"select FPRN,FTIMES,FFLFSBH,FFLFS,FFLCXBH,FFLCX,FFLZZBH,FFLZZ,FYJY,FYCS,FYTS,FYRQ1,
                            FYRQ2,FQJY,FQCS,FQTS,FQRQ1,FQRQ2,FZNAME,FZJY,FZCS,FZTS,FZRQ1,FZRQ2,FHLFSBH,
                            FHLFS,FHLFFBH,FHLFF
                            from tKnubCard d where d.fprn = ? and d.ftimes = ?";
                    #endregion

                    #region 化疗记录
                    SqlHl = @"select FPRN,FTIMES,FHLRQ1,FHLDRUG ,FHLPROC,FHLLXBH,FHLLX  from tKnubHl e where e.fprn = ? and e.ftimes = ?";
                    #endregion

                    #region 病人诊断码附加编码
                    SqlZdfj = @"select FPRN,FTIMES,FZDLX,FICDM,FFJICDM,FFJJBNAME,FFRYBQBH,FFRYBQ,FPX 
                                from TDiagnoseAdd f where f.fprn = ? and f.ftimes = ?";
                    #endregion

                    #region 中医院病人附加信息
                    SqlCh = @"select FPRN,FTIMES,FZLLBBH,FZLLB,FZZZYBH,FZZZY,FRYCYBH,FRYCY,FMZZYZDBH,
                            FMZZYZD,FSSLCLJBH,FSSLCLJ,FSYJGZJBH,FSYJGZJ,FSYZYSBBH,FSYZYSB,
                            FSYZYJSBH,FSYZYJS,FBZSHBH,FBZSH
                             from tChAdd g where g.fprn = ? and g.ftimes = ?";
                    #endregion

                    #region 条件
                    IDataParameter[] parmZd = null;
                    parmZd = svcBa.CreateParm(2);
                    parmZd[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZd[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmZk = null;
                    parmZk = svcBa.CreateParm(2);
                    parmZk[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZk[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmFop = null;
                    parmFop = svcBa.CreateParm(2);
                    parmFop[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmFop[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmFy = null;
                    parmFy = svcBa.CreateParm(2);
                    parmFy[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmFy[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmZl = null;
                    parmZl = svcBa.CreateParm(2);
                    parmZl[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZl[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmHl = null;
                    parmHl = svcBa.CreateParm(2);
                    parmHl[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmHl[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmZdfj = null;
                    parmZdfj = svcBa.CreateParm(2);
                    parmZdfj[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmZdfj[1].Value = lstUpVo[i].fpVo.FTIMES;

                    IDataParameter[] parmCh = null;
                    parmCh = svcBa.CreateParm(2);
                    parmCh[0].Value = lstUpVo[i].fpVo.FPRN;
                    parmCh[1].Value = lstUpVo[i].fpVo.FTIMES;

                    DtZd = svcBa.GetDataTable(SqlZd, parmZd);
                    DtZk = svcBa.GetDataTable(SqlZk, parmZk);
                    DtFop = svcBa.GetDataTable(SqlFop, parmFop);
                    DtFy = svcBa.GetDataTable(SqlFy, parmFy);
                    DtZl = svcBa.GetDataTable(SqlZl, parmZl);
                    DtHl = svcBa.GetDataTable(SqlHl, parmHl);
                    DtZdfj = svcBa.GetDataTable(SqlZdfj, parmZdfj);
                    DtCh = svcBa.GetDataTable(SqlCh, parmCh);

                    #endregion

                    #region 赋值
                    #region//转科信息
                    if (DtZk != null && DtZk.Rows.Count > 0)
                    {
                        EntityBrzkqk zkVo = null;
                        lstUpVo[i].fpVo.lstZkVo = new List<EntityBrzkqk>();

                        foreach (DataRow dr in DtZk.Rows)
                        {
                            zkVo = new EntityBrzkqk();

                            zkVo.FZKTYKH = dr["FZKTYKH"].ToString();
                            zkVo.FZKDEPT = dr["FZKDEPT"].ToString();
                            zkVo.FZKDATE = Function.Datetime(dr["FZKDATE"]).ToString("yyyy-MM-dd");
                            zkVo.FZKTIME = Function.Datetime(dr["FZKTIME"].ToString()).ToString("HH:mm:ss");
                            zkVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstZkVo.Add(zkVo);
                        }
                    }
                    #endregion

                    #region //诊断信息
                    if (DtZd != null && DtZd.Rows.Count > 0)
                    {
                        EntityBrzdxx zdVo = null;
                        lstUpVo[i].fpVo.lstZdVo = new List<EntityBrzdxx>();

                        foreach (DataRow dr in DtZd.Rows)
                        {
                            zdVo = new EntityBrzdxx();

                            zdVo.FZDLX = dr["FZDLX"].ToString();
                            zdVo.FICDVersion = dr["FICDVersion"].ToString();
                            zdVo.FICDM = dr["FICDM"].ToString();
                            if (dr["FJBNAME"].ToString().Length > 10)
                                zdVo.FJBNAME = dr["FJBNAME"].ToString().Substring(0, 10);
                            else
                                zdVo.FJBNAME = dr["FJBNAME"].ToString();
                            zdVo.FRYBQBH = dr["FRYBQBH"].ToString();
                            if (zdVo.FRYBQBH == "")
                                zdVo.FRYBQBH = "无";
                            zdVo.FRYBQ = dr["FRYBQ"].ToString();
                            if (zdVo.FRYBQ == "")
                                zdVo.FRYBQ = "无";
                            zdVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstZdVo.Add(zdVo);
                        }
                    }
                    #endregion

                    #region//手术信息
                    if (DtFop != null && DtFop.Rows.Count > 0)
                    {
                        EntityBrssxx fopVo = null;
                        lstUpVo[i].fpVo.lstSsVo = new List<EntityBrssxx>();

                        foreach (DataRow dr in DtFop.Rows)
                        {
                            fopVo = new EntityBrssxx();
                            fopVo.FNAME = dr["FNAME"].ToString();
                            if (fopVo.FNAME == "")
                                continue;
                            fopVo.FOPTIMES = dr["FOPTIMES"].ToString();
                            if (fopVo.FOPTIMES == "0")
                                fopVo.FOPTIMES = "1";
                            fopVo.FOPCODE = dr["FOPCODE"].ToString();
                            fopVo.FOP = dr["FOP"].ToString();
                            fopVo.FOPDATE = Function.Datetime(dr["FOPDATE"]).ToString("yyyyMMdd");
                            fopVo.FQIEKOUBH = dr["FQIEKOUBH"].ToString() == "" ? "无" : dr["FQIEKOUBH"].ToString();
                            fopVo.FQIEKOU = dr["FQIEKOU"].ToString() == "" ? "无" : dr["FQIEKOU"].ToString();
                            fopVo.FYUHEBH = dr["FYUHEBH"].ToString() == "" ? "无" : dr["FYUHEBH"].ToString();
                            if (fopVo.FYUHEBH == "")
                                fopVo.FYUHEBH = "-";
                            fopVo.FYUHE = dr["FYUHE"].ToString();
                            if (fopVo.FYUHE == "")
                                fopVo.FYUHE = "-";
                            fopVo.FDOCBH = dr["FDOCBH"].ToString();
                            if (fopVo.FDOCBH == "")
                                fopVo.FDOCBH = "-";
                            fopVo.FDOCNAME = dr["FDOCNAME"].ToString() == "" ? "无" : dr["FDOCNAME"].ToString();
                            fopVo.FMAZUIBH = dr["FMAZUIBH"].ToString();
                            if (fopVo.FMAZUIBH == "")
                                fopVo.FMAZUIBH = "无";
                            if (fopVo.FMZDOCTBH == "")
                                fopVo.FMZDOCTBH = "无";
                            fopVo.FMAZUI = dr["FMAZUI"].ToString() == "" ? "无" : dr["FMAZUI"].ToString();
                            fopVo.FIFFSOP = dr["FIFFSOP"].ToString();
                            if (fopVo.FIFFSOP == "False")
                                fopVo.FIFFSOP = "0";
                            else if (fopVo.FIFFSOP == "True")
                                fopVo.FIFFSOP = "1";
                            fopVo.FOPDOCT1BH = dr["FOPDOCT1BH"].ToString();
                            if (fopVo.FOPDOCT1BH == "")
                                fopVo.FOPDOCT1BH = "无";
                            fopVo.FOPDOCT1 = dr["FOPDOCT1"].ToString();
                            if (fopVo.FOPDOCT1 == "")
                                fopVo.FOPDOCT1 = "-";
                            fopVo.FOPDOCT2BH = dr["FOPDOCT2BH"].ToString();
                            if (fopVo.FOPDOCT2BH == "")
                                fopVo.FOPDOCT2BH = "无";
                            fopVo.FOPDOCT2 = dr["FOPDOCT2"].ToString();
                            if (fopVo.FOPDOCT2 == "")
                                fopVo.FOPDOCT2 = "无";
                            fopVo.FMZDOCTBH = dr["FMZDOCTBH"].ToString();
                            if (fopVo.FMZDOCTBH == "")
                                fopVo.FMZDOCTBH = "无";
                            fopVo.FMZDOCT = dr["FMZDOCT"].ToString();
                            if (fopVo.FMZDOCT == "")
                                fopVo.FMZDOCT = "无";
                            fopVo.FZQSSBH = dr["FZQSSBH"].ToString();
                            if (fopVo.FZQSSBH == "")
                                fopVo.FZQSSBH = "无";
                            fopVo.FZQSS = dr["FZQSS"].ToString();
                            fopVo.FSSJBBH = dr["FSSJBBH"].ToString();
                            if (fopVo.FSSJBBH == "")
                                fopVo.FSSJBBH = "无";
                            fopVo.FSSJB = dr["FSSJB"].ToString();
                            fopVo.FOPKSNAME = dr["FOPKSNAME"].ToString();
                            if (fopVo.FOPKSNAME == "")
                                fopVo.FOPKSNAME = "无";
                            fopVo.FOPTYKH = dr["FOPTYKH"].ToString();
                            if (fopVo.FOPTYKH == "")
                                fopVo.FOPTYKH = "无";

                            fopVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstSsVo.Add(fopVo);
                        }
                    }
                    #endregion

                    #region //妇婴卡信息
                    if (DtFy != null && DtFy.Rows.Count > 0)
                    {
                        EntityFyksj fyVo = null;
                        lstUpVo[i].fpVo.lstFyVo = new List<EntityFyksj>();

                        foreach (DataRow dr in DtFy.Rows)
                        {
                            fyVo = new EntityFyksj();

                            fyVo.FBABYNUM = dr["FBABYNUM"].ToString() == "" ? "-" : dr["FBABYNUM"].ToString();
                            fyVo.FNAME = dr["FNAME"].ToString() == "" ? "-" : dr["FNAME"].ToString();
                            fyVo.FBABYSEXBH = dr["FBABYSEXBH"].ToString() == "" ? "-" : dr["FBABYSEXBH"].ToString();
                            fyVo.FBABYSEX = dr["FBABYSEX"].ToString() == "" ? "-" : dr["FBABYSEX"].ToString();
                            fyVo.FTZ = dr["FTZ"].ToString() == "" ? "-" : dr["FTZ"].ToString();
                            fyVo.FRESULTBH = dr["FRESULTBH"].ToString() == "" ? "-" : dr["FRESULTBH"].ToString();
                            fyVo.FRESULT = dr["FRESULT"].ToString() == "" ? "-" : dr["FRESULT"].ToString();
                            fyVo.FZGBH = dr["FZGBH"].ToString() == "" ? "-" : dr["FZGBH"].ToString();
                            fyVo.FZG = dr["FZG"].ToString() == "" ? "-" : dr["FZG"].ToString();
                            fyVo.FBABYSUC = dr["FBABYSUC"].ToString() == "" ? "0" : dr["FBABYSUC"].ToString();
                            fyVo.FHXBH = dr["FHXBH"].ToString() == "" ? "-" : dr["FHXBH"].ToString();
                            fyVo.FHX = dr["FHX"].ToString() == "" ? "-" : dr["FHX"].ToString();
                            fyVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstFyVo.Add(fyVo);
                        }
                    }
                    #endregion

                    #region //肿瘤卡
                    if (DtZl != null && DtZl.Rows.Count > 0)
                    {
                        EntityZlksj zlVo = null;
                        lstUpVo[i].fpVo.lstZlVo = new List<EntityZlksj>();

                        foreach (DataRow dr in DtZl.Rows)
                        {
                            zlVo = new EntityZlksj();

                            zlVo.FFLFSBH = dr["FFLFSBH"].ToString();
                            zlVo.FFLFS = dr["FFLFS"].ToString();
                            zlVo.FFLCXBH = dr["FFLCXBH"].ToString();
                            zlVo.FFLCX = dr["FFLCX"].ToString();
                            zlVo.FFLZZBH = dr["FFLZZBH"].ToString();
                            zlVo.FFLZZ = dr["FFLZZ"].ToString();
                            zlVo.FYJY = dr["FYJY"].ToString();
                            zlVo.FYCS = dr["FYCS"].ToString();
                            zlVo.FYTS = dr["FYTS"].ToString();
                            zlVo.FYRQ1 = Function.Datetime(dr["FYRQ1"]).ToString("yyyyMMdd");
                            zlVo.FYRQ2 = Function.Datetime(dr["FYRQ2"]).ToString("yyyyMMdd");
                            zlVo.FQJY = dr["FQJY"].ToString();
                            zlVo.FQCS = dr["FQCS"].ToString();
                            zlVo.FQTS = dr["FQTS"].ToString();
                            zlVo.FQRQ1 = Function.Datetime(dr["FQRQ1"]).ToString("yyyyMMdd");
                            zlVo.FQRQ2 = Function.Datetime(dr["FQRQ2"]).ToString("yyyyMMdd");
                            zlVo.FZNAME = dr["FZNAME"].ToString();
                            zlVo.FZJY = dr["FZJY"].ToString();
                            zlVo.FZCS = dr["FZCS"].ToString();
                            zlVo.FZTS = dr["FZTS"].ToString();
                            zlVo.FZRQ1 = Function.Datetime(dr["FZRQ1"]).ToString("yyyyMMdd");
                            zlVo.FZRQ2 = Function.Datetime(dr["FZRQ2"]).ToString("yyyyMMdd");
                            zlVo.FHLFSBH = dr["FHLFSBH"].ToString();
                            zlVo.FHLFS = dr["FHLFS"].ToString();
                            zlVo.FHLFFBH = dr["FHLFFBH"].ToString();
                            zlVo.FHLFF = dr["FHLFF"].ToString();
                            zlVo.FPRN = dr["FPRN"].ToString();

                            if (string.IsNullOrEmpty(zlVo.FFLFSBH) || string.IsNullOrEmpty(zlVo.FHLFSBH))
                                continue;

                            lstUpVo[i].fpVo.lstZlVo.Add(zlVo);
                        }
                    }
                    #endregion

                    #region//肿瘤化疗记录
                    if (DtHl != null && DtHl.Rows.Count > 0)
                    {
                        EntityZlhljlsj hlVo = null;
                        lstUpVo[i].fpVo.lstHlVo = new List<EntityZlhljlsj>();

                        foreach (DataRow dr in DtHl.Rows)
                        {
                            hlVo = new EntityZlhljlsj();

                            hlVo.FHLRQ1 = Function.Datetime(dr["FHLRQ1"]).ToString("yyyyMMdd");
                            hlVo.FHLDRUG = dr["FHLDRUG"].ToString();
                            hlVo.FHLPROC = dr["FHLPROC"].ToString();
                            hlVo.FHLLXBH = dr["FHLLXBH"].ToString();
                            hlVo.FHLLX = dr["FHLLX"].ToString();
                            hlVo.FPRN = dr["FPRN"].ToString();
                            lstUpVo[i].fpVo.lstHlVo.Add(hlVo);
                        }
                    }
                    #endregion

                    #region//诊断
                    if (DtZdfj != null && DtZdfj.Rows.Count > 0)
                    {
                        EntityBrzdfjm zdfjVo = null;
                        lstUpVo[i].fpVo.lstZdfjVo = new List<EntityBrzdfjm>();

                        foreach (DataRow dr in DtZdfj.Rows)
                        {
                            zdfjVo = new EntityBrzdfjm();

                            zdfjVo.FZDLX = dr["FZDLX"].ToString();
                            zdfjVo.FICDM = dr["FICDM"].ToString();
                            zdfjVo.FFJICDM = dr["FFJICDM"].ToString();
                            zdfjVo.FFJJBNAME = dr["FFJJBNAME"].ToString();
                            zdfjVo.FFRYBQBH = dr["FFRYBQBH"].ToString();
                            zdfjVo.FFRYBQ = dr["FFRYBQ"].ToString();
                            zdfjVo.FPX = dr["FPX"].ToString();
                            lstUpVo[i].fpVo.lstZdfjVo.Add(zdfjVo);
                        }
                    }
                    #endregion

                    #region //中医院病人附加信息
                    if (DtCh != null && DtCh.Rows.Count > 0)
                    {
                        EntityZyybrfjxx zyVo = null;
                        lstUpVo[i].fpVo.lstZyVo = new List<EntityZyybrfjxx>();

                        foreach (DataRow dr in DtCh.Rows)
                        {
                            zyVo = new EntityZyybrfjxx();

                            zyVo.FPRN = dr["FFLFSBH"].ToString();
                            zyVo.FZLLBBH = dr["FFLFS"].ToString();
                            zyVo.FZLLB = dr["FFLCXBH"].ToString();
                            zyVo.FZZZYBH = dr["FFLCX"].ToString();
                            zyVo.FZZZY = dr["FFLZZBH"].ToString();
                            zyVo.FRYCYBH = dr["FFLZZ"].ToString();
                            zyVo.FRYCY = dr["FYJY"].ToString();
                            zyVo.FMZZYZDBH = dr["FYCS"].ToString();
                            zyVo.FMZZYZD = dr["FYTS"].ToString();
                            zyVo.FSSLCLJBH = dr["FYRQ1"].ToString();
                            zyVo.FSSLCLJ = dr["FYRQ2"].ToString();
                            zyVo.FSYJGZJBH = dr["FQJY"].ToString();
                            zyVo.FSYJGZJ = dr["FQCS"].ToString();
                            zyVo.FSYZYSBBH = dr["FQTS"].ToString();
                            zyVo.FSYZYSB = dr["FQRQ1"].ToString();
                            zyVo.FSYZYJSBH = dr["FQRQ2"].ToString();
                            zyVo.FSYZYJS = dr["FZNAME"].ToString();
                            zyVo.FBZSHBH = dr["FZJY"].ToString();
                            zyVo.FBZSH = dr["FZCS"].ToString();

                            lstUpVo[i].fpVo.lstZyVo.Add(zyVo);
                        }
                    }
                    #endregion

                    #endregion

                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetPatFirstInfo--" + e);
            }
            finally
            {
                svcBa = null;
            }
            return lstUpVo;
        }
        #endregion
        
        #region  根据ID查询员工
        public string GetEmpByID(string employeeid)
        {
            SqlHelper svc = null;
            IDataParameter[] parm = null;
            DataTable dt = null;
            string name = string.Empty;
            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                #region 
                string sql = @" select t.empno_chr,
                                       t.lastname_vchr,
                                       t.technicalrank_chr,
                                       t.pycode_chr,
                                       t.empid_chr,
                                       t.psw_chr,
                                       t.digitalsign_dta,
                                       t.technicallevel_chr
                                  from t_bse_employee t
                                 where t.status_int <> -1
                                   and t.empid_chr = ?
                                 order by t.isemployee_int desc, t.empid_chr desc";
                parm = svc.CreateParm(1);
                parm[0].Value = employeeid;
                #endregion
                dt = svc.GetDataTable(sql, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["lastname_vchr"].ToString();
                }
                else
                {
                    name = "";
                }

            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("GetEmpByID--" + e);
            }
            finally
            {
                svc = null;
            }
            return name;
        }
        #endregion

        #region 保存首页上传信息
        /// <summary>
        /// 保存首页上传信息
        /// </summary>
        /// <param name="lstVo"></param> 0 
        /// <returns></returns>
        public int SavePatFirstPage(List<EntityPatUpload> lstVo, int type = 0)
        {
            int affectRows = 0;
            decimal serNo = 0;
            string Sql = string.Empty;
            List<EntityPatUpload> lstVo1 = new List<EntityPatUpload>();
            SqlHelper svc = null;
            try
            {
                List<DacParm> lstParm = new List<DacParm>();
                svc = new SqlHelper(EnumBiz.onlineDB);

                if (lstVo.Count > 0)  // new
                {
                    foreach (EntityPatUpload item in lstVo)
                    {
                        string sql = @"select * from t_upload where REGISTERID = '" + item.REGISTERID + "'";
                        DataTable dt = svc.GetDataTable(sql);
                        if (type == 0)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                item.UPLOADDATE = DateTime.Now;
                                Sql = @"update t_upload set status = ?, UPLOADDATE = ? ,first = ?, firstMsg = ?,firstSource= ? where REGISTERID = ?";
                                if (item.Issucess == 1)
                                {
                                    IDataParameter[] parm = svc.CreateParm(6);
                                    parm[0].Value = 1;
                                    parm[1].Value = item.UPLOADDATE;
                                    parm[2].Value = 0;
                                    parm[3].Value = "";
                                    parm[4].Value = item.firstSource;
                                    parm[5].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                                else if (item.Issucess == -1)
                                {
                                    if (item.STATUS == 1 && item.first == 1)
                                        continue;
                                    IDataParameter[] parm = svc.CreateParm(6);
                                    parm[0].Value = "";
                                    parm[1].Value = item.UPLOADDATE;
                                    parm[2].Value = -1;
                                    parm[3].Value = item.FailMsg;
                                    parm[4].Value = item.firstSource;
                                    parm[5].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                            }
                            else
                            {
                                if (CheckSequence(svc, "t_upload") > 0)
                                    serNo = Function.Dec(GetNextID(svc, "t_upload").ToString());
                                item.SERNO = serNo;
                                item.UPLOADDATE = DateTime.Now;
                                item.RECORDDDATE = DateTime.Now;
                                item.OPERCODE = item.JBR;
                                if (item.Issucess == -1)
                                {
                                    item.first = -1;
                                    item.firstMsg = item.FailMsg;
                                }
                                else
                                {
                                    item.STATUS = 1;
                                }

                                lstVo1.Add(item);
                            }
                        }
                        else if (type == 1)
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                item.UPLOADDATE = DateTime.Now;
                                Sql = @"update t_upload set UPLOADDATE = ? ,xj = ?, xjMsg = ? where REGISTERID = ?";
                                if (item.Issucess == 1)
                                {
                                    IDataParameter[] parm = svc.CreateParm(4);
                                    parm[0].Value = item.UPLOADDATE;
                                    parm[1].Value = 0;
                                    parm[2].Value = "";
                                    parm[3].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                                else if (item.Issucess == -1)
                                {
                                    if (item.STATUS == 1 && item.xj == 1)
                                        continue;
                                    IDataParameter[] parm = svc.CreateParm(4);
                                    parm[0].Value = item.UPLOADDATE;
                                    parm[1].Value = -1;
                                    parm[2].Value = item.FailMsg;
                                    parm[3].Value = item.REGISTERID;
                                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                                }
                            }

                        }
                    }
                    if (lstVo1.Count > 0)
                    {
                        lstParm.Add(svc.GetInsertParm(lstVo1.ToArray()));
                    }
                    if (lstParm.Count > 0)
                        affectRows = svc.Commit(lstParm);

                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException("SavePatFirstPage--" + e);
                affectRows = -1;
            }
            finally
            {
                svc = null;
            }
            return affectRows;
        }

        #endregion

        #region  获取上传失败信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dicParm"></param>
        /// <returns></returns>
        public List<EntityPatUpload> GetFailPatList()
        {
            List<EntityPatUpload> data = new List<EntityPatUpload>();
            SqlHelper svc = null;

            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);

                string Sql1 = @"select SERNO,
                                        JZJLH,
                                        OUTHOSPITALDATE,
                                        REGISTERID,
                                        INPATIENTDATE,
                                        RECORDDDATE,
                                        INPATIENTID,
                                        PATSEX,
                                        PATNAME,
                                        firstSource,
                                        first,
                                        xj,
                                        firstMsg,
                                        xjMsg from t_upload where (first = -1 or xj= -1 )";
                DataTable dt = svc.GetDataTable(Sql1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityPatUpload vo = new EntityPatUpload();
                        vo.JZJLH = dr["JZJLH"].ToString();
                        vo.INPATIENTID = dr["INPATIENTID"].ToString();
                        vo.PATNAME = dr["PATNAME"].ToString();
                        vo.PATSEX = dr["PATSEX"].ToString();
                        vo.RYSJ = Function.Datetime(dr["INPATIENTDATE"]).ToString("yyyy-MM-dd");
                        vo.CYSJ = Function.Datetime(dr["OUTHOSPITALDATE"]).ToString("yyyy-MM-dd");
                        vo.firstMsg = dr["firstMsg"].ToString();
                        vo.xjMsg = dr["xjMsg"].ToString();
                        vo.firstSource = Function.Int(dr["firstSource"]);
                        data.Add(vo);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return data;
        }
        #endregion

        #region 获取下一个ID
        /// <summary>
        /// 获取下一个ID
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns>获取下一个ID</returns>
        public int GetNextID(SqlHelper svc, string tabName)
        {
            int intMinID = 0;
            string Sql = string.Empty;
            tabName = tabName.ToLower();
            try
            {
                if (this.CheckSequence(svc, tabName) >= 0)
                {
                    Sql = @"update sysSequenceid  set curid = curid + 1 where tabname = ?";
                    IDataParameter[] parm = svc.CreateParm(1);
                    parm[0].Value = tabName;
                    if (svc.ExecSql(Sql, parm) > 0)
                    {
                        Sql = @"select curid from sysSequenceid  where tabname = ?";
                        parm = svc.CreateParm(1);
                        parm[0].Value = tabName;

                        DataTable dt = svc.GetDataTable(Sql, parm);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            intMinID = (dt.Rows[0]["curid"] == System.DBNull.Value) ? 1 : Function.Int(dt.Rows[0]["curid"]);
                        }
                        else
                        {
                            intMinID = 1;
                        }
                    }
                    else
                    {
                        intMinID = -1;
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionLog.OutPutException(e);
                intMinID = 1;
            }
            return intMinID;
        }
        #endregion

        #region 检查
        /// <summary>
        /// 检查
        /// </summary>
        /// <param name="tabName"></param>
        /// <returns></returns>
        private int CheckSequence(SqlHelper svc, string tabName)
        {
            string Sql = @"select 1 from sysSequenceid  t where t.tabname = ?";
            IDataParameter[] parm = svc.CreateParm(1);
            parm[0].Value = tabName;
            DataTable dt = svc.GetDataTable(Sql, parm);
            if (dt == null || dt.Rows.Count == 0)
            {
                Sql = @"insert into sysSequenceid  (tabname,colname, curid) values (?, ?,?)";
                parm = svc.CreateParm(3);
                parm[0].Value = tabName;
                parm[1].Value = "serno";
                parm[2].Value = 0;
                parm[2].DbType = DbType.Int32;
                return svc.ExecSql(Sql, parm);
            }
            return 1;
        }
        #endregion

        #region 计算年龄
        /// <summary>
        /// 生日转换为年龄
        /// </summary>
        /// <param name="date"></param>
        /// <param name="yearOnly">只转换成周岁</param>
        /// <returns>不足一岁用月份表示，如6M</returns>
        public string CalcAge(DateTime? date, DateTime? inhospitalDate)
        {
            if (date == null)
                return string.Empty;

            DateTime beginDateTime = Function.Datetime(date);
            DateTime endDateTime = Function.Datetime(inhospitalDate);
            if (beginDateTime > endDateTime)
            {
                return "";
            }

            /*计算出生日期到当前日期总月数*/
            int months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);
            /*出生日期加总月数后，如果大于当前日期则减一个月*/
            int totalMonth = (beginDateTime.AddMonths(months) > endDateTime) ? months - 1 : months;
            if (totalMonth >= 12)
            {
                /*计算整年*/
                int fullYear = totalMonth / 12;
                int month = totalMonth % 12;
                if (month > 0)
                    return fullYear + "岁";
                else
                    return fullYear + "岁" + month + "月";
            }
            else
            {
                return totalMonth + "月";
            }

        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
