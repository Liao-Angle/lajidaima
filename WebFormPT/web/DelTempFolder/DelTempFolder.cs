using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using com.dsc.kernal.batch;
using com.dsc.kernal.factory;

namespace DelTempFolder
{
    public class DelFolder : AbstractBatch
    {
        protected override bool run()
        {
            string DelFolderPatch = StrSetting("DelPath");
            string[] aryFiles = Directory.GetFiles(DelFolderPatch);
            string sql;
            bool returnFlag = true;
            if (BatchStep == 1)
            {
                try
                {
                    int delCount = 0;
                    for (int i = 0; i < aryFiles.Length; i++)
                    {
                        string fileName = aryFiles[i].Substring(DelFolderPatch.Length);
                        string guid = fileName.Substring(0, fileName.LastIndexOf(".") -1);
                        sql = "select count('x') cnt from FILEITEM a WHERE FILEPATH ='" + fileName + "' AND LEVEL1='' AND LEVEL2=''";
                        int count = (int)engine.executeScalar(sql);
                        if (count == 0) //不存在草稿
                        {
                            //刪除檔案
                            File.Delete(aryFiles[i]);
                            delCount++;
                        }
                    }

                    InsertStepData("刪除檔案完成, 筆數:" + delCount);
                }
                catch (Exception e)
                {
                    InsertStepData("刪除暫存檔失敗: " + e.Message);
                    returnFlag = false;
                }
            }

            return returnFlag;
        }

        public virtual bool start(AbstractEngine m_engine, AbstractEngine engine_dll, decimal BatchStep, string BatchGUID, string BatchStepGUID, ArrayList BatchArray)
        {
            this.engine_main = m_engine;
            this.engine = engine_dll;
            this.BatchStep = BatchStep;
            this.BatchGUID = BatchGUID;
            this.BatchStepGUID = BatchStepGUID;

            try
            {
                init();
                bool errCheck = run();
                bool result = engine_main.updateDataSet(dsCAAB);

                if (errCheck && result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                InsertStepData("啟動失敗: " + e.Message);
                return false;
            }
        }
    }
}
