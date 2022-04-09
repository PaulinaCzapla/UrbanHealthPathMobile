using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    /// <summary>
    /// Represents a UI list. It consists of pages with list elements. 
    /// </summary>
    public class ListPanel : MonoBehaviour
    {
        [FormerlySerializedAs("page")] [SerializeField]
        private GameObject _page;

        [FormerlySerializedAs("backButton")] [SerializeField]
        private Button _backButton;

        [FormerlySerializedAs("forwardButton")] [SerializeField]
        private Button _forwardButton;

        [FormerlySerializedAs("frameBackButton")] [SerializeField]
        private GameObject _frameBackButton;

        [FormerlySerializedAs("frameForwardButton")] [SerializeField]
        private GameObject _frameForwardButton;

        [FormerlySerializedAs("buttonsPanel")] [SerializeField]
        private GameObject _buttonsPanel;

        [FormerlySerializedAs("pages")] [SerializeField]
        private List<GameObject> _pages = new List<GameObject>();

        [FormerlySerializedAs("parentForPages")] [SerializeField]
        private GameObject _parentForPages;

        private int _pageIndex;

        private Vector3 _position;

        private int _pagesInstantinated;

        public void Initialize(List<ListElement> elements)
        {
            int pagesCount = elements.Count / 3 + 1;
            _pagesInstantinated = _pages.Count;
            int j = 0;
            _position = Vector3.zero;

            for (int i = 0; i < pagesCount; i++)
            {
                GameObject newPage = CreateNewPage(i);
                
                List<ListElement> list = new List<ListElement>();

                for (int k = 0; k < 3; k++)
                {
                    if (j < elements.Count)
                    {
                        list.Add(elements[j]);
                        j++;
                    }
                }

                newPage.GetComponent<ListPage>().Initialize(list);
                list.Clear();
            }

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            _backButton.onClick.AddListener(GoBack);
            _forwardButton.onClick.AddListener(GoForward);
            _backButton.gameObject.SetActive(false);
            _frameBackButton.SetActive(false);
            bool isNextPage = (_pages.Count > 1 ? true : false);
            _forwardButton.gameObject.SetActive(isNextPage);
            _frameForwardButton.SetActive(isNextPage);
            _buttonsPanel.SetActive(isNextPage);
        }

        private void GoBack()
        {
            _forwardButton.gameObject.SetActive(true);
            _frameForwardButton.SetActive(true);
            _pages[_pageIndex].SetActive(false);
            _pageIndex--;
            _pages[Mathf.Clamp(_pageIndex, 0, _pages.Count - 1)].SetActive(true);

            if (_pageIndex == 0)
            {
                _backButton.gameObject.SetActive(false);
                _frameBackButton.SetActive(false);
            }
        }

        private void GoForward()
        {
            _backButton.gameObject.SetActive(true);
            _frameBackButton.SetActive(true);
            _pages[_pageIndex].SetActive(false);
            _pageIndex++;
            _pages[Mathf.Clamp(_pageIndex, 0, _pages.Count - 1)].SetActive(true);

            if (_pageIndex == _pages.Count - 1)
            {
                _forwardButton.gameObject.SetActive(false);
                _frameForwardButton.SetActive(false);
            }
        }

        private GameObject CreateNewPage(int i)
        {
            GameObject newPage;
                
            if (i + 1 > _pagesInstantinated)
            {
                newPage = Instantiate(_page);
                _pages.Add(newPage);

                var rectTrans = newPage.GetComponent<RectTransform>();
                rectTrans.SetParent(_parentForPages.transform);
                rectTrans.anchoredPosition = _position;
                rectTrans.localScale = new Vector3(1, 1, 1);
                newPage.SetActive(false);
            }
            else
            {
                newPage = _pages[i];
                var rectTrans = newPage.GetComponent<RectTransform>();
                _position = rectTrans.anchoredPosition;
            }

            return newPage;
        }

        public void OnDestroy()
        {
            _backButton.onClick.RemoveListener(GoBack);
            _forwardButton.onClick.RemoveListener(GoForward);
        }
    }
}