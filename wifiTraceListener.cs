

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace wifitracelistener
{
    public class wifiTraceListener : TraceListener
    {
        private int m_maxListViewLines;
        private ListView m_lvLog;
        private bool m_bAutoScroll;
        private TraceEventType actualLoglevel;
        private StreamWriter m_debugLogFileWriter;
        private bool m_bStopLogging = false;
        private string m_DIR_LOG;



        public wifiTraceListener(ListView lvLog, bool bAutoScroll,string _DIR_LOG, TraceEventType _actualLoglevel, int _maxListViewLines = 300)
            : base("wifiTraceListener")
        {
            m_DIR_LOG = _DIR_LOG;
            m_maxListViewLines = _maxListViewLines;
            m_lvLog = lvLog;
            m_bAutoScroll = bAutoScroll;

            if (m_lvLog != null)
            {
                m_lvLog.View = System.Windows.Forms.View.Details;
                ColumnHeader columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                columnHeader1.Width = -2;
                m_lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {columnHeader1});
               // columnHeader1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_lvLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
                m_lvLog.MultiSelect = false;

            }

            DateTime now = System.DateTime.Now;

            actualLoglevel = _actualLoglevel;


            string m_SessID = now.Ticks.ToString();

            try
            {
                if (!System.IO.Directory.Exists(m_DIR_LOG))
                {
                    System.IO.Directory.CreateDirectory(m_DIR_LOG);
                }

                m_debugLogFileWriter = System.IO.File.CreateText(m_DIR_LOG + "\\debuglog_" + m_SessID + ".txt");
            }
            catch (Exception ex)
            {

            }

            Trace.Listeners.Add(this);

        }

        public override void Close()
        {

            m_bStopLogging = true;

            try
            {
                m_debugLogFileWriter.Flush();
                m_debugLogFileWriter.Close();
                m_debugLogFileWriter = null;

            }
            catch (Exception ex)
            {

            }

        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((this.Filter != null) && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
                return;

            if (m_lvLog != null)
                WriteColorToListView(eventType, message, DateTime.Now);
            else
                WriteToFile(eventType, message, DateTime.Now);
        }



        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if ((this.Filter != null) && !this.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
                return;
            if (m_lvLog != null)
                WriteColorToListView(eventType,string.Format(format, args), DateTime.Now);
            else
                WriteToFile(eventType, string.Format(format, args), DateTime.Now);
        }



        public override void Write(string message)
        {
            if (m_lvLog != null)
                WriteColorToListView(TraceEventType.Verbose, message, DateTime.Now);
            else
                WriteToFile(TraceEventType.Verbose, message, DateTime.Now);
        }


        public override void WriteLine(string message)
        {
            if (m_lvLog != null)
                WriteColorToListView(TraceEventType.Verbose, message + Environment.NewLine, DateTime.Now);
            else
                WriteToFile(TraceEventType.Verbose, message + Environment.NewLine, DateTime.Now);

        }



        private void WriteColorToListView(TraceEventType eventType,string message,DateTime logTime)
        { 
            if (!m_lvLog.IsHandleCreated)
                return;

            if (eventType > actualLoglevel || m_bStopLogging)
                return;

            Color color;

            switch (eventType)
            {
                case TraceEventType.Error:
                    color = Color.Red;
                    break;
                case TraceEventType.Warning:
                    color = Color.DarkOrange;
                    break;
                case TraceEventType.Information:
                    color = Color.Black;
                    break;
                case TraceEventType.Verbose:
                    color = Color.Black;
                    break;
                default:
                    color = Color.Gray;
                    break;
            }

      
            m_lvLog.BeginInvoke((MethodInvoker)delegate
            {

                string[] lines = message.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                ListViewItem newItem = null;

                m_lvLog.BeginUpdate();

                foreach (string line in lines)
                {
                    if (m_lvLog.Items.Count == m_maxListViewLines)
                        m_lvLog.Items.RemoveAt(0);

                    newItem = new ListViewItem(logTime.ToString("HH:mm:ss.fffffff") + " " + line);
           
                    newItem.ForeColor = color;
                    m_lvLog.Items.Add(newItem);
                    m_lvLog.Columns[0].Width = -1;

                    try
                    {
                        if (m_debugLogFileWriter != null)
                        {
                            m_debugLogFileWriter.WriteLine(logTime.ToString("HH:mm:ss.fffffff") + " " + line);
                            m_debugLogFileWriter.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                     
                    }
                }

                if(newItem != null &&  m_bAutoScroll)
                    newItem.EnsureVisible();

                m_lvLog.EndUpdate();

            });
        }

        private void WriteToFile(TraceEventType eventType, string message,DateTime logTime)
        {
            if (m_debugLogFileWriter == null)
                return;

            if (eventType > actualLoglevel || m_bStopLogging)
                return;


            string[] lines = message.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                try
                {
                    m_debugLogFileWriter.WriteLine(logTime.ToString("HH:mm:ss.fffffff")+" "+line);
                    m_debugLogFileWriter.Flush();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
