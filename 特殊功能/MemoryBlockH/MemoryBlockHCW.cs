using Engine;
using Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreatorModAPI
{
    public class EditMemoryBankDialogAPI : Dialog
    {
        public MemoryBankData memory;

        public DynamicArray<byte> Data = new DynamicArray<byte>();

        public StackPanelWidget MainView;

        public Action onCancel;

        public int clickpos;

        public bool isSetPos;

        public int setPosN;

        public int lastvalue;

        public bool isclick = true;

        public List<ClickTextWidget> list = new List<ClickTextWidget>();

        public byte LastOutput
        {
            get;
            set;
        }

        public EditMemoryBankDialogAPI(MemoryBankData memoryBankData, Action onCancel)
        {
            memory = memoryBankData;
            Data.Clear();
            Data.AddRange(memory.Data);
            CanvasWidget canvasWidget = new CanvasWidget
            {
                Size = new Vector2(600f, 500f),
                HorizontalAlignment = WidgetAlignment.Center,
                VerticalAlignment = WidgetAlignment.Center
            };
            RectangleWidget widget = new RectangleWidget
            {
                FillColor = new Color(0, 0, 0, 255),
                OutlineColor = new Color(128, 128, 128, 128),
                OutlineThickness = 2f
            };
            StackPanelWidget stackPanelWidget = new StackPanelWidget
            {
                Direction = LayoutDirection.Vertical
            };
            LabelWidget widget2 = new LabelWidget
            {
                Text = LanguageControl.GetContentWidgets(GetType().Name, 0),
                HorizontalAlignment = WidgetAlignment.Center,
                Margin = new Vector2(0f, 10f)
            };
            StackPanelWidget stackPanelWidget2 = new StackPanelWidget
            {
                Direction = LayoutDirection.Horizontal,
                HorizontalAlignment = WidgetAlignment.Near,
                VerticalAlignment = WidgetAlignment.Near,
                Margin = new Vector2(10f, 10f)
            };
            Children.Add(canvasWidget);
            canvasWidget.Children.Add(widget);
            canvasWidget.Children.Add(stackPanelWidget);
            stackPanelWidget.Children.Add(widget2);
            stackPanelWidget.Children.Add(stackPanelWidget2);
            stackPanelWidget2.Children.Add(initData());
            stackPanelWidget2.Children.Add(initButton());
            //q-in
            Led_Init();
            Led_Load(canvasWidget);
            MainView = stackPanelWidget;
            this.onCancel = onCancel;
            lastvalue = memory.Read(0);
        }

        public byte Read(int address)
        {
            if (address >= 0 && address < Data.Count)
            {
                return Data.Array[address];
            }

            return 0;
        }

        public void Write(int address, byte data)
        {
            if (address >= 0 && address < Data.Count)
            {
                Data.Array[address] = data;
            }
            else if (address >= 0 && address < 256 && data != 0)
            {
                Data.Count = MathUtils.Max(Data.Count, address + 1);
                Data.Array[address] = data;
            }
        }

        public void LoadString(string data)
        {
            string[] array = data.Split(new char[1]
            {
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length >= 1)
            {
                string text = array[0];
                text = text.TrimEnd('0');
                Data.Clear();
                for (int i = 0; i < MathUtils.Min(text.Length, 256); i++)
                {
                    int num = MemoryBankData.m_hexChars.IndexOf(char.ToUpperInvariant(text[i]));
                    if (num < 0)
                    {
                        num = 0;
                    }

                    Data.Add((byte)num);
                }
            }

            if (array.Length >= 2)
            {
                string text2 = array[1];
                int num2 = MemoryBankData.m_hexChars.IndexOf(char.ToUpperInvariant(text2[0]));
                if (num2 < 0)
                {
                    num2 = 0;
                }

                LastOutput = (byte)num2;
            }
        }

        public string SaveString()
        {
            return SaveString(saveLastOutput: true);
        }

        public string SaveString(bool saveLastOutput)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int count = Data.Count;
            for (int i = 0; i < count; i++)
            {
                int index = MathUtils.Clamp(Data.Array[i], 0, 15);
                stringBuilder.Append(MemoryBankData.m_hexChars[index]);
            }

            if (saveLastOutput)
            {
                stringBuilder.Append(';');
                stringBuilder.Append(MemoryBankData.m_hexChars[MathUtils.Clamp(LastOutput, 0, 15)]);
            }

            return stringBuilder.ToString();
        }

        public Widget initData()
        {
            StackPanelWidget stackPanelWidget = new StackPanelWidget
            {
                Direction = LayoutDirection.Vertical,
                VerticalAlignment = WidgetAlignment.Center,
                HorizontalAlignment = WidgetAlignment.Far,
                Margin = new Vector2(10f, 0f)
            };
            for (int i = 0; i < 17; i++)
            {
                StackPanelWidget stackPanelWidget2 = new StackPanelWidget
                {
                    Direction = LayoutDirection.Horizontal
                };
                for (int j = 0; j < 17; j++)
                {
                    int addr = (i - 1) * 16 + (j - 1);
                    if (j > 0 && i > 0)
                    {
                        ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(22f), $"{MemoryBankData.m_hexChars[Read(addr)]}", delegate
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            clickpos = addr;
                            isclick = true;
                        });
                        list.Add(clickTextWidget);
                        stackPanelWidget2.Children.Add(clickTextWidget);
                        continue;
                    }

                    int num = 0;
                    if (i == 0 && j > 0)
                    {
                        num = j - 1;
                    }
                    else
                    {
                        if (j != 0 || i <= 0)
                        {
                            ClickTextWidget widget = new ClickTextWidget(new Vector2(22f), "", null);
                            stackPanelWidget2.Children.Add(widget);
                            continue;
                        }

                        num = i - 1;
                    }

                    ClickTextWidget clickTextWidget2 = new ClickTextWidget(new Vector2(22f), MemoryBankData.m_hexChars[num].ToString(), delegate
                    {
                    });
                    clickTextWidget2.labelWidget.Color = Color.DarkGray;
                    stackPanelWidget2.Children.Add(clickTextWidget2);
                }

                stackPanelWidget.Children.Add(stackPanelWidget2);
            }

            return stackPanelWidget;
        }

        public Widget makeFuncButton(string txt, Action func)
        {
            return new ClickTextWidget(new Vector2(40f), txt, func, box: true)
            {
                BorderColor = Color.White,
                Margin = new Vector2(2f),
                labelWidget =
                {
                    FontScale = ((txt.Length > 1) ? 0.7f : 1f),
                    Color = Color.White
                }
            };
        }

        public Widget initButton()
        {
            StackPanelWidget stackPanelWidget = new StackPanelWidget
            {
                Direction = LayoutDirection.Vertical,
                VerticalAlignment = WidgetAlignment.Center,
                HorizontalAlignment = WidgetAlignment.Far,
                Margin = new Vector2(10f, 10f)
            };
            for (int i = 0; i < 6; i++)
            {
                StackPanelWidget stackPanelWidget2 = new StackPanelWidget
                {
                    Direction = LayoutDirection.Horizontal
                };
                for (int j = 0; j < 3; j++)
                {
                    int num = i * 3 + j;
                    if (num < 15)
                    {
                        int pp = num + 1;
                        stackPanelWidget2.Children.Add(makeFuncButton($"{MemoryBankData.m_hexChars[pp]}", delegate
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            if (!isSetPos)
                            {
                                Write(clickpos, (byte)pp);
                                lastvalue = pp;
                                clickpos++;
                                if (clickpos >= 255)
                                {
                                    clickpos = 0;
                                }

                                isclick = true;
                            }
                            else
                            {
                                if (setPosN == 0)
                                {
                                    clickpos = 16 * pp;
                                }
                                else if (setPosN == 1)
                                {
                                    clickpos += pp;
                                }

                                setPosN++;
                                if (setPosN == 2)
                                {
                                    if (clickpos > 255)
                                    {
                                        clickpos = 0;
                                    }

                                    setPosN = 0;
                                    isclick = true;
                                    isSetPos = false;
                                }
                            }
                        }));
                        continue;
                    }

                    switch (num)
                    {
                        case 15:
                            stackPanelWidget2.Children.Add(makeFuncButton($"{MemoryBankData.m_hexChars[0]}", delegate
                            {
                                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                                if (!isSetPos)
                                {
                                    Write(clickpos, 0);
                                    lastvalue = 0;
                                    clickpos++;
                                    if (clickpos >= 255)
                                    {
                                        clickpos = 0;
                                    }

                                    isclick = true;
                                }
                                else
                                {
                                    if (setPosN == 0)
                                    {
                                        clickpos = 0;
                                    }
                                    else if (setPosN == 1)
                                    {
                                        //clickpos +=0;
                                    }

                                    setPosN++;
                                    if (setPosN == 2)
                                    {
                                        if (clickpos > 255)
                                        {
                                            clickpos = 0;
                                        }

                                        setPosN = 0;
                                        isclick = true;
                                        isSetPos = false;
                                    }
                                }
                            }));
                            break;
                        case 16:
                            stackPanelWidget2.Children.Add(makeFuncButton(LanguageControl.GetContentWidgets(GetType().Name, 1), delegate
                            {
                                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                                for (int l = 0; l < Data.Count; l++)
                                {
                                    Write(l, 0);
                                }

                                isclick = true;
                            }));
                            break;
                        case 17:
                            stackPanelWidget2.Children.Add(makeFuncButton(LanguageControl.GetContentWidgets(GetType().Name, 2), delegate
                            {
                                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                                DynamicArray<byte> dynamicArray = new DynamicArray<byte>();
                                dynamicArray.AddRange(Data);
                                dynamicArray.Count = 256;
                                for (int m = 0; m < 16; m++)
                                {
                                    for (int n = 0; n < 16; n++)
                                    {
                                        Write(m + n * 16, dynamicArray[m * 16 + n]);
                                    }
                                }

                                clickpos = 0;
                                isclick = true;
                            }));
                            break;
                    }
                }

                stackPanelWidget.Children.Add(stackPanelWidget2);
            }

            LabelWidget widget = new LabelWidget
            {
                FontScale = 0.8f,
                Text = LanguageControl.GetContentWidgets(GetType().Name, 3),
                HorizontalAlignment = WidgetAlignment.Center,
                Margin = new Vector2(0f, 10f),
                Color = Color.DarkGray
            };
            stackPanelWidget.Children.Add(widget);
            stackPanelWidget.Children.Add(makeTextBox(delegate (TextBoxWidget textBoxWidget)
            {
                LoadString(textBoxWidget.Text);
                isclick = true;
            }, memory.SaveString(saveLastOutput: false)));
            stackPanelWidget.Children.Add(makeButton(LanguageControl.GetContentWidgets(GetType().Name, 4), delegate
            {
                for (int k = 0; k < Data.Count; k++)
                {
                    memory.Write(k, Data[k]);
                }

                onCancel?.Invoke();
                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                DialogsManager.HideDialog(this);
            }));
            stackPanelWidget.Children.Add(makeButton(LanguageControl.GetContentWidgets(GetType().Name, 5), delegate
            {
                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                DialogsManager.HideDialog(this);
                isclick = true;
            }));
            return stackPanelWidget;
        }

        public Widget makeTextBox(Action<TextBoxWidget> ac, string text = "")
        {
            CanvasWidget obj = new CanvasWidget
            {
                HorizontalAlignment = WidgetAlignment.Center
            };
            RectangleWidget widget = new RectangleWidget
            {
                FillColor = Color.Black,
                OutlineColor = Color.White,
                Size = new Vector2(120f, 30f)
            };
            StackPanelWidget stackPanelWidget = new StackPanelWidget
            {
                Direction = LayoutDirection.Vertical
            };
            TextBoxWidget textBoxWidget = new TextBoxWidget
            {
                VerticalAlignment = WidgetAlignment.Center,
                Color = new Color(255, 255, 255),
                Margin = new Vector2(4f, 0f),
                Size = new Vector2(120f, 30f),
                MaximumLength = 256
            };
            textBoxWidget.FontScale = 0.7f;
            textBoxWidget.Text = text;
            textBoxWidget.TextChanged += ac;
            stackPanelWidget.Children.Add(textBoxWidget);
            obj.Children.Add(widget);
            obj.Children.Add(stackPanelWidget);
            return obj;
        }

        public Widget makeButton(string txt, Action tas)
        {
            return new ClickTextWidget(new Vector2(120f, 30f), txt, tas)
            {
                BorderColor = Color.White,
                Margin = new Vector2(0f, 3f),
                labelWidget =
                {
                    FontScale = 0.7f,
                    Color = Color.Green
                }
            };
        }

        public override void Update()
        {
            if (base.Input.Back || base.Input.Cancel)
            {
                DialogsManager.HideDialog(this);
            }

            if (isSetPos)
            {
                list[clickpos].BorderColor = Color.Red;
            }
            else
            {
                if (!isclick)
                {
                    return;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    if (i == clickpos)
                    {
                        list[i].BorderColor = Color.Yellow;
                        //q-in
                        Led_SetColor(i);
                    }
                    else
                    {
                        list[i].BorderColor = Color.Transparent;
                    }

                    list[i].labelWidget.Text = $"{MemoryBankData.m_hexChars[Read(i)]}";
                }

                isclick = false;
            }
        }
        private readonly RectangleWidget[] Led_light = new RectangleWidget[4];
        private void Led_Init()
        {
            int temp = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Led_light[temp] = new RectangleWidget
                    {
                        Size = new Vector2(15, 15),
                        Margin = new Vector2(j * 15, i * 15),
                        OutlineColor = Color.Violet,
                        FillColor = Color.White,
                        IsVisible = true
                    };
                    temp++;
                }
            }

        }
        private void Led_Load(CanvasWidget canvasWidget)
        {
            canvasWidget.Children.Add(Led_light);
        }
        private void Led_SetColor(int IsLight_Value)
        {
            Led_light[0].FillColor = ((Read(IsLight_Value) & 1) >> 0 == 1) ? Color.White : Color.Black;
            Led_light[1].FillColor = ((Read(IsLight_Value) & 2) >> 1 == 1) ? Color.White : Color.Black;
            Led_light[2].FillColor = ((Read(IsLight_Value) & 4) >> 2 == 1) ? Color.White : Color.Black;
            Led_light[3].FillColor = ((Read(IsLight_Value) & 8) >> 3 == 1) ? Color.White : Color.Black;
        }
    }
}
/*
using Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class EditMemeryDialogH : Dialog
    {

        //Led_light
        private readonly RectangleWidget[] Led_light = new RectangleWidget[4];
        //@
        public MemoryBankData memory;
        public DynamicArray<byte> Data = new DynamicArray<byte>();
        public StackPanelWidget MainView;
        public Action onCancel;
        public int clickpos = 0;

        public bool isSetPos = false;//是否为设定位置模式
        public int setPosN = 0;//第几位数
        public int lastvalue = 0;
        public bool isclick = true;
        public static string fName = "EditMemeryDialogB";
        public List<ClickTextWidget> list = new List<ClickTextWidget>();

        public byte LastOutput
        {
            get;
            set;
        }

        public EditMemeryDialogH(MemoryBankData memoryBankData, Action onCancel)
        {
            memory = memoryBankData;
            Data.Clear();
            Data.AddRange(memory.Data);
            CanvasWidget canvasWidget = new CanvasWidget() { Size = new Vector2(600f, 500f), HorizontalAlignment = WidgetAlignment.Center, VerticalAlignment = WidgetAlignment.Center };
            RectangleWidget rectangleWidget = new RectangleWidget() { FillColor = new Color(0, 0, 0, 255), OutlineColor = new Color(128, 128, 128, 128), OutlineThickness = 2 };
            StackPanelWidget stackPanel = new StackPanelWidget() { Direction = LayoutDirection.Vertical };
            LabelWidget labelWidget = new LabelWidget() { Text = LanguageControl.GetContentWidgets(fName, 0), HorizontalAlignment = WidgetAlignment.Center, Margin = new Vector2(0, 10) };
            StackPanelWidget stackPanelWidget = new StackPanelWidget() { Direction = LayoutDirection.Horizontal, HorizontalAlignment = WidgetAlignment.Near, VerticalAlignment = WidgetAlignment.Near, Margin = new Vector2(10f, 10f) };
            //ledlight
            int temp = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Led_light[temp] = new RectangleWidget
                    {
                        Size = new Vector2(15, 15),
                        Margin = new Vector2(j * 15, i * 15),
                        OutlineColor = Color.Violet,
                        FillColor = Color.White,
                        IsVisible = true
                    };
                    temp++;
                }
            }
            //@
            Children.Add(canvasWidget);
            canvasWidget.Children.Add(rectangleWidget);
            canvasWidget.Children.Add(stackPanel);
            stackPanel.Children.Add(labelWidget);
            stackPanel.Children.Add(stackPanelWidget);
            stackPanelWidget.Children.Add(InitData());
            stackPanelWidget.Children.Add(InitButton());
            //ledlight
            canvasWidget.Children.Add(Led_light);
            //@
            MainView = stackPanel;
            this.onCancel = onCancel;
            lastvalue = memory.Read(0);
        }
        public byte Read(int address)
        {
            if (address >= 0 && address < Data.Count)
            {
                return Data.Array[address];
            }
            return 0;
        }

        public void Write(int address, byte data)
        {
            if (address >= 0 && address < Data.Count)
            {
                Data.Array[address] = data;
            }
            else if (address >= 0 && address < 256 && data != 0)
            {
                Data.Count = MathUtils.Max(Data.Count, address + 1);
                Data.Array[address] = data;
            }
        }
        public void LoadString(string data)
        {
            string[] array = data.Split(new char[1]
            {
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length >= 1)
            {
                string text = array[0];
                text = text.TrimEnd('0');
                Data.Clear();
                for (int i = 0; i < MathUtils.Min(text.Length, 256); i++)
                {
                    int num = MemoryBankData.m_hexChars.IndexOf(char.ToUpperInvariant(text[i]));
                    if (num < 0)
                    {
                        num = 0;
                    }
                    Data.Add((byte)num);
                }
            }
            if (array.Length >= 2)
            {
                string text2 = array[1];
                int num2 = MemoryBankData.m_hexChars.IndexOf(char.ToUpperInvariant(text2[0]));
                if (num2 < 0)
                {
                    num2 = 0;
                }
                LastOutput = (byte)num2;
            }
        }

        public string SaveString()
        {
            return SaveString(saveLastOutput: true);
        }

        public string SaveString(bool saveLastOutput)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = Data.Count;
            for (int j = 0; j < num; j++)
            {
                int index = MathUtils.Clamp(Data.Array[j], 0, 15);
                stringBuilder.Append(MemoryBankData.m_hexChars[index]);
            }
            if (saveLastOutput)
            {
                stringBuilder.Append(';');
                stringBuilder.Append(MemoryBankData.m_hexChars[MathUtils.Clamp(LastOutput, 0, 15)]);
            }
            return stringBuilder.ToString();
        }

        public Widget InitData()
        {
            StackPanelWidget stack = new StackPanelWidget() { Direction = LayoutDirection.Vertical, VerticalAlignment = WidgetAlignment.Center, HorizontalAlignment = WidgetAlignment.Far, Margin = new Vector2(10, 0) };
            for (int i = 0; i < 17; i++)
            {
                StackPanelWidget line = new StackPanelWidget() { Direction = LayoutDirection.Horizontal };
                for (int j = 0; j < 17; j++)
                {
                    int addr = (i - 1) * 16 + (j - 1);
                    if (j > 0 && i > 0)
                    {
                        ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(22), string.Format("{0}", MemoryBankData.m_hexChars[Read(addr)]), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            clickpos = addr;
                            isclick = true;
                        });
                        list.Add(clickTextWidget);
                        line.Children.Add(clickTextWidget);
                    }
                    else
                    {
                        int p = 0;
                        if (i == 0 && j > 0)
                        {
                            p = j - 1;
                        }
                        else if (j == 0 && i > 0)
                        {
                            p = i - 1;
                        }
                        else
                        {
                            ClickTextWidget click = new ClickTextWidget(new Vector2(22), "", null);
                            line.Children.Add(click);
                            continue;
                        };
                        ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(22), MemoryBankData.m_hexChars[p].ToString(), delegate ()
                        {

                        });
                        clickTextWidget.labelWidget.Color = Color.DarkGray;
                        line.Children.Add(clickTextWidget);
                    }
                }
                stack.Children.Add(line);
            }
            return stack;
        }

        public Widget MakeFuncButton(string txt, Action func)
        {
            ClickTextWidget clickText = new ClickTextWidget(new Vector2(40), txt, func, true)
            {
                BorderColor = Color.White,
                Margin = new Vector2(2)
            };
            clickText.labelWidget.FontScale = txt.Length > 1 ? 0.7f : 1f;
            clickText.labelWidget.Color = Color.White;
            return clickText;
        }

        public Widget InitButton()
        {
            StackPanelWidget stack = new StackPanelWidget() { Direction = LayoutDirection.Vertical, VerticalAlignment = WidgetAlignment.Center, HorizontalAlignment = WidgetAlignment.Far, Margin = new Vector2(10, 10) };
            for (int i = 0; i < 6; i++)
            {
                StackPanelWidget stackPanelWidget = new StackPanelWidget() { Direction = LayoutDirection.Horizontal };
                for (int j = 0; j < 3; j++)
                {
                    int cc = i * 3 + j;
                    if (cc < 15)
                    {
                        int pp = cc + 1;
                        stackPanelWidget.Children.Add(MakeFuncButton(string.Format("{0}", MemoryBankData.m_hexChars[pp]), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            if (!isSetPos)
                            {
                                Write(clickpos, (byte)pp);//写入数据
                                lastvalue = pp;
                                clickpos += 1;//自动加1
                                if (clickpos >= 255)
                                {
                                    clickpos = 0;
                                }
                                isclick = true;
                            }
                            else
                            { //处于设定位置模式
                                if (setPosN == 0)
                                {
                                    clickpos = 16 * pp;
                                }
                                else if (setPosN == 1)
                                {
                                    clickpos += pp;
                                }

                                setPosN += 1;
                                if (setPosN == 2)
                                {
                                    if (clickpos > 0xff)
                                    {
                                        clickpos = 0;
                                    }

                                    setPosN = 0;
                                    isclick = true;
                                    isSetPos = false;
                                }
                            }
                        }));
                    }
                    else if (cc == 15)
                    {
                        stackPanelWidget.Children.Add(MakeFuncButton(string.Format("{0}", MemoryBankData.m_hexChars[0]), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            if (!isSetPos)
                            {
                                Write(clickpos, 0);//写入数据
                                lastvalue = 0;
                                clickpos += 1;//自动加1
                                if (clickpos >= 255)
                                {
                                    clickpos = 0;
                                }
                                isclick = true;
                            }
                            else
                            { //处于设定位置模式
                                if (setPosN == 0)
                                {
                                    clickpos = 0;
                                }
                                else if (setPosN == 1)
                                {
                                    clickpos += 0;
                                }

                                setPosN += 1;
                                if (setPosN == 2)
                                {
                                    if (clickpos > 0xff)
                                    {
                                        clickpos = 0;
                                    }

                                    setPosN = 0;
                                    isclick = true;
                                    isSetPos = false;
                                }
                            }
                        }));
                        continue;
                    }
                    else if (cc == 16)
                    {
                        stackPanelWidget.Children.Add(MakeFuncButton(LanguageControl.GetContentWidgets(fName, 1), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            for (int ai = 0; ai < Data.Count; ai++)
                            {
                                Write(ai, 0);
                            }
                            isclick = true;

                        }));
                        continue;
                    }
                    else if (cc == 17)
                    {
                        stackPanelWidget.Children.Add(MakeFuncButton(LanguageControl.GetContentWidgets(fName, 2), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            DynamicArray<byte> tmp = new DynamicArray<byte>();
                            tmp.AddRange(Data);
                            tmp.Count = 256;
                            for (int c = 0; c < 16; c++)
                            {
                                for (int d = 0; d < 16; d++)
                                {
                                    Write(c + d * 16, tmp[c * 16 + d]);
                                }
                            }
                            clickpos = 0;
                            isclick = true;
                        }));
                        continue;
                    }
                }
                stack.Children.Add(stackPanelWidget);
            }
            LabelWidget labelWidget = new LabelWidget() { FontScale = 0.8f, Text = LanguageControl.GetContentWidgets(fName, 3), HorizontalAlignment = WidgetAlignment.Center, Margin = new Vector2(0f, 10f), Color = Color.DarkGray };
            stack.Children.Add(labelWidget);
            stack.Children.Add(MakeTextBox(delegate (TextBoxWidget textBoxWidget)
            {
                LoadString(textBoxWidget.Text);
                isclick = true;
            }, memory.SaveString(false)));
            stack.Children.Add(MakeButton(LanguageControl.GetContentWidgets(fName, 4), delegate ()
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    memory.Write(i, Data[i]);
                }
                onCancel?.Invoke();
                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                DialogsManager.HideDialog(this);
            }));
            stack.Children.Add(MakeButton(LanguageControl.GetContentWidgets(fName, 5), delegate ()
            {
                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                DialogsManager.HideDialog(this);
                isclick = true;
            }));
            return stack;
        }

        public Widget MakeTextBox(Action<TextBoxWidget> ac, string text = "")
        {
            CanvasWidget canvasWidget = new CanvasWidget() { HorizontalAlignment = WidgetAlignment.Center };
            RectangleWidget rectangleWidget = new RectangleWidget() { FillColor = Color.Black, OutlineColor = Color.White, Size = new Vector2(120, 30) };
            StackPanelWidget stack = new StackPanelWidget() { Direction = LayoutDirection.Vertical };
            TextBoxWidget textBox = new TextBoxWidget() { VerticalAlignment = WidgetAlignment.Center, Color = new Color(255, 255, 255), Margin = new Vector2(4f, 0f), Size = new Vector2(120, 30), MaximumLength = 256 };
            textBox.FontScale = 0.7f;
            textBox.Text = text;
            textBox.TextChanged += ac;
            stack.Children.Add(textBox);
            canvasWidget.Children.Add(rectangleWidget);
            canvasWidget.Children.Add(stack);
            return canvasWidget;
        }

        public Widget MakeButton(string txt, Action tas)
        {
            ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(120, 30), txt, tas)
            {
                BorderColor = Color.White,
                Margin = new Vector2(0, 3)
            };
            clickTextWidget.labelWidget.FontScale = 0.7f;
            clickTextWidget.labelWidget.Color = Color.Green;
            return clickTextWidget;
        }

        public override void Update()
        {
            if (Input.Back || Input.Cancel)
            {
                DialogsManager.HideDialog(this);
            }
            if (isSetPos)
            {
                list[clickpos].BorderColor = Color.Red;//设定选择颜色
                return;
            }
            if (!isclick)
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (i == clickpos)
                {
                    list[i].BorderColor = Color.Yellow;//设定选择颜色
                    Led_light[0].FillColor = ((Read(i) & 1) >> 0 == 1) ? Color.White : Color.Black;
                    Led_light[1].FillColor = ((Read(i) & 2) >> 1 == 1) ? Color.White : Color.Black;
                    Led_light[2].FillColor = ((Read(i) & 4) >> 2 == 1) ? Color.White : Color.Black;
                    Led_light[3].FillColor = ((Read(i) & 8) >> 3 == 1) ? Color.White : Color.Black;
                }
                else
                {
                    list[i].BorderColor = Color.Transparent;//设定选择颜色
                }
                list[i].labelWidget.Text = string.Format("{0}", MemoryBankData.m_hexChars[Read(i)]);

            }
            isclick = false;
        }
    }
}
*/

/*
using CreatorModAPI;
using Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Game
{
    public class EditMemeryDialogH : Dialog
    {
        public MemoryBankData memory;
        public DynamicArray<byte> Data = new DynamicArray<byte>();
        public StackPanelWidget MainView;
        public Action onCancel;
        public int clickpos = 0;
        private RectangleWidget[] Led_light = new RectangleWidget[4];
        public bool isSetPos = false;//是否为设定位置模式
        public int setPosN = 0;//第几位数
        public int lastvalue = 0;
        public bool isclick = true;
        public static string fName = "EditMemeryDialogB";
        public List<ClickTextWidget> list = new List<ClickTextWidget>();

        public EditMemeryDialogH(MemoryBankData memoryBankData, Action onCancel)
        {
            memory = memoryBankData;
            Data.Clear();
            Data.AddRange(memory.Data);
            CanvasWidget canvasWidget = new CanvasWidget() { Size = new Vector2(600f, float.PositiveInfinity), HorizontalAlignment = WidgetAlignment.Center, VerticalAlignment = WidgetAlignment.Center };
            RectangleWidget rectangleWidget = new RectangleWidget() { FillColor = new Color(0, 0, 0, 255), OutlineColor = new Color(128, 128, 128, 128), OutlineThickness = 2 };
            StackPanelWidget stackPanel = new StackPanelWidget() { Direction = LayoutDirection.Vertical };
            LabelWidget labelWidget = new LabelWidget() { Text = LanguageControl.GetContentWidgets(fName, 0), HorizontalAlignment = WidgetAlignment.Center, Margin = new Vector2(0, 10) };
            StackPanelWidget stackPanelWidget = new StackPanelWidget() { Direction = LayoutDirection.Horizontal, HorizontalAlignment = WidgetAlignment.Near, VerticalAlignment = WidgetAlignment.Near, Margin = new Vector2(10f, 10f) };

            int temp = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Led_light[temp] = new RectangleWidget
                    {
                        Size = new Vector2(15, 15),
                        Margin = new Vector2(j * 15, i * 15),
                        OutlineColor = Color.Violet,
                        FillColor = Color.White,
                        IsVisible = true
                    };
                    temp++;
                }
            }

            Children.Add(canvasWidget);
            canvasWidget.Children.Add(rectangleWidget);
            canvasWidget.Children.Add(stackPanel);
            stackPanel.Children.Add(labelWidget);
            stackPanel.Children.Add(stackPanelWidget);
            stackPanelWidget.Children.Add(initData());
            stackPanelWidget.Children.Add(initButton());
            canvasWidget.Children.Add(Led_light);
            MainView = stackPanel;
            this.onCancel = onCancel;
            lastvalue = memory.Read(0);
        }
        public byte Read(int address)
        {
            if (address >= 0 && address < Data.Count)
            {
                return Data.Array[address];
            }
            return 0;
        }

        public void Write(int address, byte data)
        {
            if (address >= 0 && address < Data.Count)
            {
                Data.Array[address] = data;
            }
            else if (address >= 0 && address < 256 && data != 0)
            {
                Data.Count = MathUtils.Max(Data.Count, address + 1);
                Data.Array[address] = data;
            }
        }
        public void LoadString(string data)
        {
            string[] array = data.Split(new char[1]
            {
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length >= 1)
            {
                string text = array[0];
                text = text.TrimEnd('0');
                Data.Clear();
                for (int i = 0; i < MathUtils.Min(text.Length, 256); i++)
                {
                    int num = MemoryBankData.m_hexChars.IndexOf(char.ToUpperInvariant(text[i]));
                    if (num < 0)
                    {
                        num = 0;
                    }
                    Data.Add((byte)num);
                }
            }
            if (array.Length >= 2)
            {
                string text2 = array[1];
                int num2 = MemoryBankData.m_hexChars.IndexOf(char.ToUpperInvariant(text2[0]));
                if (num2 < 0)
                {
                    num2 = 0;
                }
                LastOutput = (byte)num2;
            }
        }

        public string SaveString()
        {
            return SaveString(saveLastOutput: true);
        }
        public byte LastOutput
        {
            get;
            set;
        }
        public string SaveString(bool saveLastOutput)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = Data.Count;
            for (int j = 0; j < num; j++)
            {
                int index = MathUtils.Clamp(Data.Array[j], 0, 15);
                stringBuilder.Append(MemoryBankData.m_hexChars[index]);
            }
            if (saveLastOutput)
            {
                stringBuilder.Append(';');
                stringBuilder.Append(MemoryBankData.m_hexChars[MathUtils.Clamp(LastOutput, 0, 15)]);
            }
            return stringBuilder.ToString();
        }
        public Widget initData()
        {
            StackPanelWidget stack = new StackPanelWidget() { Direction = LayoutDirection.Vertical, VerticalAlignment = WidgetAlignment.Center, HorizontalAlignment = WidgetAlignment.Far, Margin = new Vector2(10, 0) };
            for (int i = 0; i < 17; i++)
            {
                StackPanelWidget line = new StackPanelWidget() { Direction = LayoutDirection.Horizontal };
                for (int j = 0; j < 17; j++)
                {
                    int addr = (i - 1) * 16 + (j - 1);
                    if (j > 0 && i > 0)
                    {
                        ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(22), string.Format("{0}", MemoryBankData.m_hexChars[Read(addr)]), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            clickpos = addr;
                            isclick = true;
                        });
                        list.Add(clickTextWidget);
                        line.Children.Add(clickTextWidget);
                    }
                    else
                    {
                        int p = 0;
                        if (i == 0 && j > 0) p = j - 1;
                        else if (j == 0 && i > 0) p = i - 1;
                        else
                        {
                            ClickTextWidget click = new ClickTextWidget(new Vector2(22), "", null);
                            line.Children.Add(click);
                            continue;
                        };
                        ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(22), MemoryBankData.m_hexChars[p].ToString(), delegate ()
                        {

                        });
                        clickTextWidget.labelWidget.Color = Color.DarkGray;
                        line.Children.Add(clickTextWidget);
                    }
                }
                stack.Children.Add(line);
            }
            return stack;
        }

        public Widget makeFuncButton(string txt, Action func)
        {
            ClickTextWidget clickText = new ClickTextWidget(new Vector2(50, 40), txt, func, true);
            clickText.Margin = new Vector2(5, 2);
            clickText.labelWidget.FontScale = 1f;
            clickText.labelWidget.Color = Color.Black;
            return clickText;
        }
        public Widget initButton()
        {
            StackPanelWidget stack = new StackPanelWidget() { Direction = LayoutDirection.Vertical };
            for (int i = 0; i < 6; i++)
            {
                StackPanelWidget stackPanelWidget = new StackPanelWidget() { Direction = LayoutDirection.Horizontal };
                for (int j = 0; j < 3; j++)
                {
                    int cc = i * 3 + j;
                    if (cc < 15)
                    {
                        int pp = cc + 1;
                        stackPanelWidget.Children.Add(makeFuncButton(string.Format("{0}", MemoryBankData.m_hexChars[pp]), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            if (!isSetPos)
                            {
                                Write(clickpos, (byte)pp);//写入数据
                                lastvalue = pp;
                                clickpos += 1;//自动加1
                                if (clickpos >= 255)
                                {
                                    clickpos = 0;
                                }
                                isclick = true;
                            }
                            else
                            { //处于设定位置模式
                                if (setPosN == 0) clickpos = 16 * pp;
                                else if (setPosN == 1) clickpos += pp;
                                setPosN += 1;
                                if (setPosN == 2)
                                {
                                    if (clickpos > 0xff) clickpos = 0;
                                    setPosN = 0;
                                    isclick = true;
                                    isSetPos = false;
                                }
                            }
                        }));
                    }
                    else if (cc == 15)
                    {
                        stackPanelWidget.Children.Add(makeFuncButton(string.Format("{0}", MemoryBankData.m_hexChars[0]), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            if (!isSetPos)
                            {
                                Write(clickpos, (byte)0);//写入数据
                                lastvalue = 0;
                                clickpos += 1;//自动加1
                                if (clickpos >= 255)
                                {
                                    clickpos = 0;
                                }
                                isclick = true;
                            }
                            else
                            { //处于设定位置模式
                                if (setPosN == 0) clickpos = 0;
                                else if (setPosN == 1) clickpos += 0;
                                setPosN += 1;
                                if (setPosN == 2)
                                {
                                    if (clickpos > 0xff) clickpos = 0;
                                    setPosN = 0;
                                    isclick = true;
                                    isSetPos = false;
                                }
                            }
                        }));
                        continue;
                    }
                    else if (cc == 16)//qing0
                    {
                        stackPanelWidget.Children.Add(makeFuncButton(LanguageControl.GetContentWidgets(fName, 1), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            for (int ai = 0; ai < Data.Count; ai++)
                            {
                                Write(ai, 0);
                            }
                            isclick = true;

                        }));
                        continue;
                    }
                    else if (cc == 17)//zhuanzhi
                    {
                        stackPanelWidget.Children.Add(makeFuncButton(LanguageControl.GetContentWidgets(fName, 2), delegate ()
                        {
                            AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                            DynamicArray<byte> tmp = new DynamicArray<byte>();
                            tmp.AddRange(Data);
                            tmp.Count = 256;
                            for (int c = 0; c < 16; c++)
                            {
                                for (int d = 0; d < 16; d++)
                                {
                                    Write(c + d * 16, tmp[c * 16 + d]);
                                }
                            }
                            clickpos = 0;
                            isclick = true;
                        }));
                        continue;
                    }
                }
                stack.Children.Add(stackPanelWidget);
            }
            LabelWidget labelWidget = new LabelWidget() { FontScale = 0.8f, Text = LanguageControl.GetContentWidgets(fName, 3), HorizontalAlignment = WidgetAlignment.Center, Margin = new Vector2(0f, 10f), Color = Color.DarkGray };
            stack.Children.Add(labelWidget);
            stack.Children.Add(makeTextBox(delegate (TextBoxWidget textBoxWidget)
            {
                LoadString(textBoxWidget.Text);
                isclick = true;
            }, memory.SaveString(false)));
            stack.Children.Add(makeButton(LanguageControl.GetContentWidgets(fName, 4), delegate ()
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    memory.Write(i, Data[i]);
                }
                onCancel?.Invoke();
                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                DialogsManager.HideDialog(this);
            }));
            stack.Children.Add(makeButton(LanguageControl.GetContentWidgets(fName, 5), delegate ()
            {
                AudioManager.PlaySound("Audio/UI/ButtonClick", 1f, 0f, 0f);
                DialogsManager.HideDialog(this);
                isclick = true;
            }));
            return stack;
        }
        public Widget makeTextBox(Action<TextBoxWidget> ac, string text = "")
        {
            CanvasWidget canvasWidget = new CanvasWidget() { HorizontalAlignment = WidgetAlignment.Center };
            RectangleWidget rectangleWidget = new RectangleWidget() { FillColor = Color.Black, OutlineColor = Color.White, Size = new Vector2(120, 30) };
            StackPanelWidget stack = new StackPanelWidget() { Direction = LayoutDirection.Vertical };
            TextBoxWidget textBox = new TextBoxWidget() { VerticalAlignment = WidgetAlignment.Center, Color = new Color(255, 255, 255), Margin = new Vector2(4f, 0f), Size = new Vector2(120, 30), MaximumLength = 256 };
            textBox.FontScale = 0.7f;
            textBox.Text = text;
            textBox.TextChanged += ac;
            stack.Children.Add(textBox);
            canvasWidget.Children.Add(rectangleWidget);
            canvasWidget.Children.Add(stack);
            return canvasWidget;
        }
        public Widget makeButton(string txt, Action tas)
        {
            ClickTextWidget clickTextWidget = new ClickTextWidget(new Vector2(120, 30), txt, tas);
            clickTextWidget.rectangleWidget.OutlineColor = Color.White;
            //   clickTextWidget.BackGround = Color.Gray;
            clickTextWidget.rectangleWidget.OutlineThickness = 2;
            clickTextWidget.Margin = new Vector2(0, 3);
            clickTextWidget.labelWidget.FontScale = 0.7f;
            clickTextWidget.labelWidget.Color = Color.Green;
            return clickTextWidget;
        }

        public override void Update()
        {
            int i = 0;
            if (isSetPos)
            {
                list[clickpos].rectangleWidget.OutlineColor = Color.Red;//设定选择颜色
                list[clickpos].rectangleWidget.OutlineThickness = 1;
                return;
            }
            if (!isclick) return;
            foreach (ClickTextWidget clickText in list)
            {
                if (i == clickpos)
                {
                    list[i].rectangleWidget.OutlineColor = Color.Yellow;//设定选择颜色
                    list[i].rectangleWidget.OutlineThickness = 1;
                    this.Led_light[0].FillColor = ((Read(i) & 1) >> 0 == 1) ? Color.White : Color.Black;
                    this.Led_light[1].FillColor = ((Read(i) & 2) >> 1 == 1) ? Color.White : Color.Black;
                    this.Led_light[2].FillColor = ((Read(i) & 4) >> 2 == 1) ? Color.White : Color.Black;
                    this.Led_light[3].FillColor = ((Read(i) & 8) >> 3 == 1) ? Color.White : Color.Black;
                }
                else
                {
                    list[i].rectangleWidget.OutlineColor = Color.Transparent;//设定选择颜色
                }

                list[i].labelWidget.Text = string.Format("{0}", MemoryBankData.m_hexChars[Read(i)]);
                list[i].IsDrawRequired = false;
                ++i;
            }
            isclick = false;
        }
    }
}*/