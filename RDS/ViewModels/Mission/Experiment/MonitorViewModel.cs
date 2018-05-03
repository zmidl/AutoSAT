using RDS.Models.RuntimeData.Base;
using RDS.Models.RuntimeData.WorkPanel;
using RDS.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using PopupMode = RDS.ViewModels.Common.PopupMode;

namespace RDS.ViewModels.Mission.Experiment
{
    public class MonitorViewModel : ViewModel
    {
        public AutoSAT RuntimeData { get; set; }

        public CountdownTimer CountdownTimer { get; set; }

        public bool FinishTaskState { get { return this.IsTaskStarted ? false : true; } }

        private bool isEmergencyStopped = false;

        public bool IsUnlock
        {
            get { return General.IsUnLock; }
            private set
            {
                General.IsUnLock = value;
                this.RaisePropertyChanged(nameof(this.IsUnlock));

                this.LockPath = value ? General.UnLock : General.Lock;
                this.RaisePropertyChanged(nameof(this.LockPath));
            }
        }

        private bool isWicketOpened = false;

        public bool SamplingResult { get; set; }

        public bool StripResult { get; set; }

        public bool StripLoadingResult { get; set; }

        public List<double> THValue { get; private set; } = new List<double>(4) { 0d, 0d, 0d, 0d };

        public PathGeometry TaskPath { get; set; } = General.Play;

        public PathGeometry LockPath { get; set; } = General.UnLock;

        private bool isTaskStarted;
        public bool IsTaskStarted
        {
            get { return isTaskStarted; }
            set
            {
                if (value)
                {
                    // General.SDK.CheckDoorStatus(1);
                    // General.SDK.Unlock(false);
                }
                //else General.SDK.Unlock(true);
                if (this.DispatchTask())
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.RefreshTaskStateToUi(value);
                        this.isTaskStarted = value;
                        this.RaisePropertyChanged(nameof(IsTaskStarted));
                        this.Unlock.RaiseCanExecuteChanged();
                    });
                }
                this.RaisePropertyChanged(nameof(this.FinishTaskState));
            }
        }

        public enum ViewChangedOption
        {
            ShowSampleView = 0,
            ShowStripView = 1,
            ShowEmergencyView = 2,
            ShowTipView = 3,
            TaskStop = 4,
            TaskPause = 5,
            ShowReagentView = 6,
            NotifySamplingResult = 7
        }

        public class MonitorViewChangedArgs : EventArgs
        {
            public ViewChangedOption Option { get; set; }
            public object Value { get; set; }

            public MonitorViewChangedArgs(ViewChangedOption option, object value)
            {
                this.Option = option;
                this.Value = value;
            }
        }

        public RelayCommand Unlock => new RelayCommand(this.ExecuteUnlock, this.CanExecuteUnlock);

        public RelayCommand HTU => new RelayCommand(() => this.TEST_HeatingTemperature(true));

        public RelayCommand HTD => new RelayCommand(() => this.TEST_HeatingTemperature(false));

        public RelayCommand Test1 => new RelayCommand(() => this.CountdownTimer.StartTimer());

        public RelayCommand Test2 => new RelayCommand(() => this.CountdownTimer.StopTimer());

        public RelayCommand Test3 => new RelayCommand(() => this.CountdownTimer.ChangeTimer(new Random().Next(1, 10), new Random().Next(1, 10)));

        public RelayCommand ShowStripView => new RelayCommand(() =>
        {
            if (this.isTaskStarted) General.PopupWindow(General.FindStringResource(Properties.Resources.Message1), new PopupMode[] { PopupMode.Ok });
            else this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowStripView, null));
        });

        public RelayCommand SetTip => new RelayCommand((o) => this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowTipView, o)));

        public RelayCommand TaskControl => new RelayCommand(() => this.IsTaskStarted = !this.IsTaskStarted);

        public MonitorViewModel()
        {
            this.InitializeData();

            this.InitializeUI();

            this.InitializeSampleRack();

            this.InitializeStripGroups();
        }

        private void InitializeData()
        {
            this.RuntimeData = General.RuntimeData;

            this.CountdownTimer = new CountdownTimer(9, 56, 59);
        }

        private void InitializeUI()
        {

        }

        private void InitializeSampleRack()
        {
            for (int i = 0; i < 4; i++)
            {
                this.RuntimeData.WorkPanel.Areas[0].Models[i].SetSampleHoleNumber();
            }
        }

        private void RefreshTaskStateToUi(bool isTaskStart)
        {
            if (isTaskStart) { this.CountdownTimer.StartTimer(); this.TaskPath = General.Pause; }
            else { this.CountdownTimer.StopTimer(); this.TaskPath = General.Play; }
            this.RaisePropertyChanged(nameof(this.TaskPath));
        }

        private bool DispatchTask()
        {
            var result = true;
            //switch (General.SDK.RobotState)
            //{
            //    case RobotState.Idle: { result = this.StartOrContinueTask(true); break; }
            //    case RobotState.ArmPause:
            //    case RobotState.Pause: { result = this.StartOrContinueTask(false); break; }
            //    case RobotState.Running: { this.PauseTask(); break; }
            //    default: break;
            //}
            return result;
        }

        private void PauseTask()
        {
            // General.SDK.Pause();
            //if (this.IsUnlock) General.PopupWindow(General.FindStringResource(Properties.Resources.Message11), new PopupMode[] { PopupMode.Ok });
        }
        private bool StartOrContinueTask(bool isStartOrContinue)
        {
            if (true)
            {
                this.CountdownTimer.StartTimer();
                return true;
            }
            else
            {
                var result = false;
                if (true) General.PopupWindow(General.FindStringResource(Properties.Resources.Message15), new PopupMode[] { PopupMode.Ok });
                else if (true) General.PopupWindow(General.FindStringResource(Properties.Resources.Message16), new PopupMode[] { PopupMode.Ok });
                //else if (this.SamplingResult == false) General.PopupWindow(PopupMode.OneButton, PopupType.Alert, General.FindStringResource(Properties.Resources.PopupWindow_Message2));
                //else if (this.StripResult == false) General.PopupWindow(PopupMode.OneButton, PopupType.Alert, General.FindStringResource(Properties.Resources.Message3));
                else if (this.IsUnlock || this.isWicketOpened) General.PopupWindow(General.FindStringResource(Properties.Resources.Message11), new PopupMode[] { PopupMode.Ok });
                //else if (this.TipViewModel.GateLock.IsLocked == false) General.PopupWindow(General.FindStringResource(Properties.Resources.Message12), new PopupMode[] { PopupMode.Ok });
                else
                {
                    if (isStartOrContinue)
                    {
                        //General.SDK.workbench.lastTipStates = General.SDK.workbench.GetTipStates();

                        //Task.Factory.StartNew(() => { General.SDK.LoopExecute(); });
                    }
                    //else General.SDK.Continue();
                    result = true;
                }
                return result;
            }
        }

        private void ExecuteUnlock()
        {
            //General.SDK.Unlock(this.IsUnlock = true);
            //if (this.IsUnlock == false) General.SDK.Unlock(this.IsUnlock = true);
        }

        private bool CanExecuteUnlock()
        {
            return this.IsTaskStarted == false;
        }

        //public void ShowTipView(RdCore.Enum.TipType tipType)
        //{
        //    if (this.IsTaskStarted) this.IsTaskStarted = false;
        //    var message = string.Empty;
        //    switch (tipType)
        //    {
        //        case RdCore.Enum.TipType.Tip300:
        //        {

        //            message = General.FindStringResource(Properties.Resources.Message8);
        //            break;
        //        }
        //        case RdCore.Enum.TipType.Tip1000:
        //        {

        //            message = General.FindStringResource(Properties.Resources.Message9);
        //            break;
        //        }
        //        default: break;
        //    }

        //    var action = new Action(() => {; });

        //    General.PopupWindow(message, new PopupMode[] { PopupMode.Ok });
        //}

        public RelayCommand ShowReagentView
        {
            get => new RelayCommand(() =>
            {
                this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowReagentView, null));
            });
        }
        public RelayCommand ShowSampleView
        {
            get => new RelayCommand(() =>
            {
                if (this.IsTaskStarted) this.IsTaskStarted = false;
                this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowSampleView, this.IsTaskStarted));
            });
        }

        public RelayCommand ShowEmergencyView
        {
            get => new RelayCommand(() =>
            {
                this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.ShowEmergencyView, null));
            });
        }

        private void UpdateTemperatureAndHumidity(List<double> data)
        {
            this.THValue = data;
            this.RaisePropertyChanged(nameof(this.THValue));
        }

        private void UpdateModuleTemperature(double heartingTemperrature, double readerTemperature)
        {

        }

        private void InitializeStripGroups()
        {

        }

        public RelayCommand FinishTask
        {
            get
            {
                return new RelayCommand(() =>
                {
                    General.PopupWindow
                    (
                        "是否离开当前任务",
                        new PopupMode[] { PopupMode.Ok, PopupMode.Cancel },
                        new Action[]
                        {
                            new Action(()=>
                            {
                                //this.PauseRemainingTimer();
                                //this.remainingTime = new DateTime(yearUnit, monthUnit, dateUnit, hourUnit, minuteUnit, secondUnit);
                                //this.RaisePropertyChanged(nameof(this.RemainingTime));

                                this.isTaskStarted = false;
                                this.RaisePropertyChanged(nameof(this.IsTaskStarted));

                                this.TaskPath = General.Play;
                                this.RaisePropertyChanged(nameof(this.TaskPath));
                                this.FinishTask.RaiseCanExecuteChanged();
                                //General.SDK.Unlock(this.IsUnlock = true);
                                this.OnViewChanged(new MonitorViewChangedArgs(ViewChangedOption.TaskStop, null));
                            }),
                            null
                        }
                     );
                });
            }
        }

        public void SetSamplingResult(bool samplingResult)
        {
            //this.SamplingResult = samplingResult;
            //var customNapCount = App.GlobalData.Naps.Where(o => o.IsCurrentNapExist == 1).Count();
            //if (App.GlobalData.UsedNapCount <= customNapCount && customNapCount != 0) this.SetStripResult(true);
            //else this.SetStripResult(false);
        }

        public void SetStripResult(bool stripResult)
        {
            this.StripResult = stripResult;

            this.RaisePropertyChanged(nameof(this.StripResult));
        }

        private void SetSingleTipState(int rackIndex, int tipIndex, bool isLoaded)
        {

        }

        private void SetMultiTipState(int rackIndex, bool[] isTipsLoaded)
        {

        }

        private void SetTipRackState(int rackIndex, bool isLoaded)
        {

        }

        private void UpdateTipState(int[,] tipLocations)
        {
            for (int i = 0; i < 3; i++)
            {
                var rackIndex = tipLocations[i, 0];
                if (rackIndex >= 0) this.SetSingleTipState(rackIndex, tipLocations[i, 1], false);
            }
        }



        private void StripMoved(int from, int to)
        {

        }

        private void StripMoving(int from, int to)
        {

        }

        //private void UpdateStripCellState(UiEvent action, int stripsId, string cellsState)
        //{
        //    var id = stripsId - 1;
        //    var a = id / 7;
        //    var b = id % 7;
        //    for (int i = 0; i < cellsState.Length; i++)
        //    {
        //        if (action == UiEvent.Dispense)
        //        {
        //            if (cellsState.Substring(i, 1) == Properties.Resources.One)
        //            {

        //            }
        //        }
        //        else if (action == UiEvent.Waste)
        //        {
        //            if (cellsState.Substring(i, 1) == Properties.Resources.Zero)
        //            {

        //            }
        //        }
        //    }
        //}

        private void UpdateReagentState(Dictionary<int, int> dictionary)
        {
            var keys = dictionary.Keys.ToArray();
            var values = dictionary.Values.ToArray();
            for (int i = 0; i < dictionary.Count; i++)
            {
                //this.SetReagentState(keys[i], values[i]);
            }
        }

        private void UpdateShakerState(bool isShak)
        {

        }

        private Tuple<int, int> GetStripLocation(int index)
        {
            if (index < 1) return new Tuple<int, int>(7, 0);
            else if (index > 38) index = 38;
            int tenth = 0;
            int units = 0;
            if (index > 32) { tenth = 6; units = index % 33; }
            else if (index > 28) { tenth = 5; units = index % 29; }
            else if (index > 25) { tenth = 4; units = index % 26; }
            else if (index > 21) { tenth = 3; units = index % 22; }
            else if (index > 14) { tenth = 2; units = index % 15; }
            else if (index > 7) { tenth = 1; units = index % 8; }
            else { tenth = 0; units = index - 1; }
            return new Tuple<int, int>(tenth, units);
        }

        private void SDK_DoorStatusChanged(int door, int status)
        {
            // 如果是大门变更
            if (door == 0) this.IsUnlock = status == 1 ? true : false;
            else if (door == 2)
            {
                // 按下急停
                if (status == 0)
                {
                    if (this.isEmergencyStopped == false)
                    {
                        General.PopupWindow
                        (
                            "仪器紧急停止中",
                            new PopupMode[] { PopupMode.Retry },
                            new Action[] { new Action(() =>/* General.SDK.CheckDoorStatus(2)*/{; }) }
                        );
                    }
                }
            }
            else
            {
                if (status == 1)
                {
                    this.isWicketOpened = true;
                    if (this.IsTaskStarted) this.IsTaskStarted = false;
                }
                else this.isWicketOpened = false;
            }
        }

        //private void Workbench_RefreshUiEvent(UiEvent action, object obj1, object obj2)
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        switch (action)
        //        {
        //            case UiEvent.Moving: { this.StripMoving((int)obj1, (int)obj2); break; }
        //            case UiEvent.MoveItem: { this.StripMoved((int)obj1, (int)obj2); break; }
        //            case UiEvent.Aspirate: { this.UpdateReagentState((Dictionary<int, int>)obj2); break; }
        //            case UiEvent.Dispense:
        //            case UiEvent.Waste: { this.UpdateStripCellState(action, (int)obj1, obj2.ToString()); break; }
        //            case UiEvent.Shake: { this.UpdateShakerState((bool)obj1); break; }
        //            case UiEvent.GetTip: { this.UpdateTipState((int[,])obj1); break; }
        //            case UiEvent.Thy: { this.UpdateTemperatureAndHumidity((List<double>)obj1); break; }
        //            case UiEvent.Temperature: { this.UpdateModuleTemperature((double)obj1, (double)obj2); break; }
        //            case UiEvent.Finished: { this.FinishTask.Execute(null); break; }
        //            default: break;
        //        }
        //    });
        //}
        //private void Workbench_NotEnoughTipsEvent(RdCore.Enum.TipType tipType)
        //{
        //    this.ShowTipView(tipType);
        //}

        public void TEST_HeatingTemperature(bool a)
        {
            this.RuntimeData.WorkPanel.Areas[2].Models[0].Slots[0].IsLoaded = a;
            this.RuntimeData.WorkPanel.Areas[2].Models[0].Slots[1].IsLoaded = true;
            this.RuntimeData.WorkPanel.Areas[2].Models[0].Slots[0].IsMove = true;
            this.RuntimeData.WorkPanel.Areas[2].Models[0].Slots[1].IsMove = false;
            //this.RuntimeData.WorkPanel.Areas[3].Models[0].Slots[0].Tube.Cavities[0].IsLoaded = a;

            //this.StripMoved(1, 25);
        }

        private void SubscribeEvent()
        {

        }
        private void UnsubscribeEvent()
        {

        }
    }

    public class CountdownTimer : Notify
    {
        private readonly int interval = 1000;
        private readonly int year = 1;
        private readonly int month = 1;
        private readonly int date = 1;
        private int hour = 0;
        private int minute = 0;
        private int second = 0;
        private DateTime time;
        private System.Timers.Timer timer;
        public string CountdownData { get { return time.ToString(Properties.Resources.RemainingTimeFormat); } }

        public CountdownTimer(int hour, int minute, int sencond)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = sencond;
            this.time = new DateTime(year, month, date, this.hour, this.minute, this.second);
            this.InitializeTimer();
        }

        private void InitializeTimer()
        {
            this.timer = new System.Timers.Timer(this.interval);
            this.timer.Elapsed += (sender, args) =>
            {
                this.time = this.time.AddSeconds(-1);
                this.RaisePropertyChanged(nameof(CountdownData));
            };
            this.timer.AutoReset = true;
        }

        public void ChangeTimer(int hour, int minute)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = this.time.Second;
            this.time = new DateTime(year, month, date, this.hour, this.minute, this.second);
            this.RaisePropertyChanged(nameof(CountdownData));
        }

        public void StartTimer()
        {
            this.timer.Enabled = true;
            this.timer.Start();
        }

        public void StopTimer()
        {
            this.timer.Stop();
        }
    }
}