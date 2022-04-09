namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    /// <summary>
    /// Interface that determines a view/popup that can be displayed.
    /// </summary>
    public interface IDisplayable
    {
        //Created with fade in, fade out animations in mind. 
        public void Display();
        public void Hide();
    }
}