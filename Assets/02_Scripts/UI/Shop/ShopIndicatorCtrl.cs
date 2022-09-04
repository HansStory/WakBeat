public class ShopIndicatorCtrl : ArrayCtrl<ShopIndicator>
{
    private SHOPKIND m_shopKind = SHOPKIND.SKIN;

    public SHOPKIND Current { get { return m_shopKind; } }

    protected override void Init()
    {
        
    }

    public void ChangeIndicator(SHOPKIND kind)
    {
        m_shopKind = kind;
        for(int i = 0; i < m_arr.Length; i++)
        {
            m_arr[i].Enabled = i == (int)kind;
        }
    }
}
