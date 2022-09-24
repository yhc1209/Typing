using System.Diagnostics;

namespace Typing;

public partial class Form1 : Form
{
    const int GEN_WORD_PERIOD = 1500;   // unit: ms
    const int FPS = 10;
    const int UPPER_BASE = 30, LOWER_BASE = 5;
    private List<AWord> words;
    private int gravity;
    private int during; // unit: ms
    private SolidBrush mBrsh, nBrsh, bBrsh;
    private string input = string.Empty;
    private System.Windows.Forms.Timer tmr;
    private Random rdm = new Random(DateTime.Now.GetHashCode());
    private List<string> dictionary;
    public Form1()
    {
        InitializeComponent();
        Font = new Font("Courier New", 15, FontStyle.Bold);
        mBrsh = new SolidBrush(Color.Red);
        nBrsh = new SolidBrush(Color.DarkBlue);
        bBrsh = new SolidBrush(Color.Black);

        dictionary = new List<string>(Globals.SomeWords);
        words = new List<AWord>() {
            new AWord("hello", Font, 20, UPPER_BASE),
            new AWord("world", Font, 160, UPPER_BASE),
        };

        DrawTimes();
        gravity = 1;
        during = 0;
        tmr = new System.Windows.Forms.Timer();
        tmr.Interval = 1000/FPS;
        tmr.Tick += (o, e) => {
            if (dictionary.Count > 0)
            {
                if (during%(GEN_WORD_PERIOD) < 1000/FPS)
                    words.Add(ARandomWord());
            }
            else
            {
                if (words.Count == 0)
                {
                    tmr.Stop();
                    MessageBox.Show("沒了", "啪");
                    Close();
                }
            }
            Invalidate();
            during += 1000/FPS;
        };
        tmr.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        DrawTimes();
        DrawTexts();
        base.OnPaint(e);
    }

    private void DrawTimes()
    {
        Graphics g = CreateGraphics();
        g.Clear(BackColor);
        Pen p = new Pen(Color.Black);
        g.DrawRectangle(p, 10, UPPER_BASE, ClientSize.Width - 20, ClientSize.Height - (UPPER_BASE + LOWER_BASE));
        g.DrawString($"DURING: {during/1000:D4} sec", Font, bBrsh, 5, 5);
    }

    private void DrawTexts()
    {
        Graphics g = CreateGraphics();
        float spd = (float)gravity/(float)FPS;
        foreach (AWord w in words)
        {
            // drawtext
            if (w.matchNum > 0)
            {
                string strA = w.word.Substring(0, w.matchNum);
                string strB = new String(' ', w.matchNum) + w.word.Substring(w.matchNum);
                g.DrawString(strB, Font, nBrsh, w.pos_x, w.pos_y);
                g.DrawString(strA, Font, mBrsh, w.pos_x, w.pos_y);
            }
            else
                g.DrawString(w.word, Font, nBrsh, w.pos_x, w.pos_y);

            // next position
            if (w.pos_y + spd*w.area.Height < ClientSize.Height - w.area.Height - 5)
                w.pos_y = w.pos_y + spd*(float)w.area.Height;
            else
                w.pos_y = ClientSize.Height - w.area.Height - LOWER_BASE;
        }
    }

    private AWord? ARandomWord()
    {
        if (dictionary.Count == 0)
            return null;

        int idx = rdm.Next(dictionary.Count);
        string w = dictionary[idx];
        dictionary.RemoveAt(idx);
        int x = rdm.Next(ClientSize.Width - TextRenderer.MeasureText(w, Font).Width);
        return new AWord(w, Font, x, UPPER_BASE);
    }

    private void Form_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (Char.IsLetter(e.KeyChar))
        {
            input += e.KeyChar;
            Debug.WriteLine($"intput=\'{input}\'");
        }
        else
            return;

        bool bNoMatch = true;
        foreach (AWord w in words)
        {
            if (w.word.Length < input.Length)
                continue;

            if (w.word.Substring(0, input.Length) == input)
            {
                bNoMatch = false;
                w.matchNum = input.Length;
            }
            else
                w.matchNum = 0;
        }

        if (bNoMatch || words.RemoveAll(w => { return (w.word == input); }) > 0)
            input = string.Empty;
    }
}
