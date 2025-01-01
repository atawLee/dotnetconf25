﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Data;
using Newtonsoft.Json;
// ReSharper disable All

namespace BMS;

public partial class ManualGradingForm : UserControl
{
    private readonly List<ExamInformation> _gradeProgressList = new(100);
    private readonly List<GradingInfo> _gradingInfoList = new(100);
    public ManualGradingForm()
    {
        InitializeComponent();
        datagridGradeProgressList.DataSource = _gradeProgressList;
    }

    private async void btnSearch_Click(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        var result = await client.GetAsync("https://sampledata");
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<List<ExamInformation>>(json);
        _gradeProgressList.Clear();
        _gradeProgressList.AddRange(data);
    }

    private async void datagridGradeProgressList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        var gradeProgressInfo = _gradeProgressList[e.RowIndex];
        HttpClient client = new HttpClient();
        var result = await client.GetAsync("https://sampledata");
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<List<GradingInfo>>(json);
        _gradingInfoList.Clear();
        _gradingInfoList.AddRange(data);
        this.tabControl1.SelectedIndex = 1;

        // 첫 번째 행 선택
        if (datagridAnswer.Rows.Count > 0)
        {
            datagridAnswer.Rows[0].Selected = true;  // 첫 번째 행 선택
            datagridAnswer.CurrentCell = datagridAnswer.Rows[0].Cells[0]; // 포커스 설정
        }
    }

    private void datagridAnswer_SelectionChanged(object sender, EventArgs e)
    {

    }
}