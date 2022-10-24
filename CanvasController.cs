using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Leopotam.EcsLite;
using DG.Tweening;
using System;
using Firebase.Analytics;

namespace Client
{
    public class CanvasController : MonoBehaviour
    {
        private GameState _state;
        private EcsWorld _world;
        [Header("Game Panels")]
        [SerializeField] private GameObject _beforeStartPanel;
        [SerializeField] public GameObject _panelStart;
        [SerializeField] private GameObject _panelLose;
        [SerializeField] private GameObject _panelWin;
        [SerializeField] private GameObject _panelScore;
        [SerializeField] private GameObject _panelGame;
        [SerializeField] private GameObject _panelNewHero;
        [SerializeField] private GameObject _settignsPanel;
        [SerializeField] private GameObject _tutorialHeroPanel;
        [SerializeField] private GameObject _sundukPanel;
        private Image[] _sundukImages = new Image[9];
        private Text[] _sundukText = new Text[9];
        [Header("Attributes")]
        [SerializeField] public GameObject _ultimateMelee;
        [SerializeField] private GameObject _ultimateRange;
        [SerializeField] private Text _textCoins;
        [SerializeField] private Image _meleeButtonImage;
        [SerializeField] private Image _rangeButtonImage;
        [SerializeField] private Image[] _rankUltimates;
        [SerializeField] private GameObject[] _panelNewHeroImages;
        [SerializeField] private Text _damageInfo;
        [SerializeField] private Text _healthInfo;
        [SerializeField] private Text _nameInfo;
        [SerializeField] private Image _newHeroImage;
        [SerializeField] private GameObject[] _settingButtons;
        [SerializeField] public GameObject[] _gameButtons;
        [SerializeField] private ParticleSystem[] _particleSystems;
        public Image[] ImageCooldown;
        [Header("Level Settings")]
        [SerializeField] private Text _levelText;
        [SerializeField] private Transform _progressImageTransform;
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private Sprite _pointSprite;
        [SerializeField] private Image _currentBiomImage;
        [SerializeField] private Image _nextBiomImage;
        [SerializeField] private Transform _biomPointHolder;
        [SerializeField] private GameObject _biomPointPrefab;
        [SerializeField] private Text _rewardeText;
        [SerializeField] private GameObject _rewardedPanel;
        [Header("Rewards")]
        [SerializeField] private GameObject _spinObject;
        [SerializeField] private GameObject _cancelAdsWinEvent;
        [SerializeField] private GameObject _cancelAdsChestAndKeys;
        [SerializeField] private GameObject _adsButton;
        [SerializeField] private GameObject _reward;
        [SerializeField] private GameObject _onlyRewardText;
        [SerializeField] private GameObject _nextLevel;
        [SerializeField] private GameObject _rewardHolder;
        [SerializeField] private Text _keysText;
        [SerializeField] private GameObject[] _keys;
        [SerializeField] private GameObject[] _rewards;
        [SerializeField] private int _currentKeys;
        [SerializeField] private GameObject _failedRewardPanel;
        [SerializeField] private GameObject _EXITBUTTON;
        [SerializeField] private GameObject[] _freeButtons;
        private int _oppenedChest;
        private Image[] _pointsImage;
        private EcsPool<UltimateEvent> _ultimatePool;
        private EcsPool<SpinEventComponent> _spinPool;
        private EcsPool<StopSpinEventComponent> _spinStopPool;
        private EcsPool<LevelRawardComponent> _rewardPool;
        private bool isEncounter;
        private bool isStart;
        private bool isSpining;
        private bool isRewardEnd;
        private EcsPool<TutorialComponent> _tutorialPool;
        private EcsPool<OpenSundukEvent> _openSundukPool;
        public bool CanUsePlay = true;
        public bool CanUseUltimate = true;
        public bool CanUseBuyButton = true;
        public bool isTutorial = false;
        private bool _playingLevel = false;
        
        private EcsPool<PlayInterstitialEvent> _interstitialPool;
        private EcsPool<AddHeroByADSEvent> _addHeroByADSPool;
        private EcsPool<MultiplyCoinsByADSEvent> _multiplyCoinsByADSPool;
        private EcsPool<AddKeysByADSEvent> _addKeysByADSPool;
        private EcsPool<VibrationEvent> _vibrationPool;
        #region Initialization
        public void Init(GameState state, EcsWorld world)
        {
            _state = state;
            _world = world;
            isStart = false;
            _ultimatePool = _world.GetPool<UltimateEvent>();
            _tutorialPool = _world.GetPool<TutorialComponent>();
            _openSundukPool = _world.GetPool<OpenSundukEvent>();
            _spinPool = _world.GetPool<SpinEventComponent>();
            _spinStopPool = _world.GetPool<StopSpinEventComponent>();
            _rewardPool = world.GetPool<LevelRawardComponent>();
            _interstitialPool = world.GetPool<PlayInterstitialEvent>();
            _addHeroByADSPool = world.GetPool<AddHeroByADSEvent>();
            _multiplyCoinsByADSPool = world.GetPool<MultiplyCoinsByADSEvent>();
            _addKeysByADSPool = world.GetPool<AddKeysByADSEvent>();
            _vibrationPool = world.GetPool<VibrationEvent>();
            SettingsPanelStart();
            CheckActiveColor();
            InitBiomPoints();
            InitSundukImages();
        }
        public void InitSundukImages()
        {
            var t = _sundukPanel.transform.GetChild(1).transform;
            for (int i = 0; i < _sundukImages.Length; i++)
            {
                _sundukImages[i] = t.GetChild(i).transform.GetChild(3).GetComponent<Image>();
                _sundukText[i] = t.GetChild(i).transform.GetChild(4).GetComponent<Text>();
            }
            //sunduk
        }
        public void InitProgressPoint(int count)
        {
            _pointsImage = new Image[count];
            for (int i = 0; i < _pointsImage.Length; i++)
            {
                _pointsImage[i] = Instantiate(_pointPrefab, _progressImageTransform).GetComponent<Image>();
            }
        }

        public void InitBiomPoints()
        {
            _currentBiomImage.sprite = _state.CurrentBiom.BiomSprite;
            _nextBiomImage.sprite = _state.CurrentBiom.NextBiomSprite;

            for (int i = 0; i < _state.CurrentBiom.BiomLevels.Count; i++)
            {
                var lvl = _state.CurrentBiom.BiomLevels[i];
                var currentScene = SceneManager.GetActiveScene().buildIndex;
                if (currentScene < lvl)
                {
                    var image = Instantiate(_biomPointPrefab, _biomPointHolder).GetComponent<Image>();
                    image.color = _state.LevelsStorage.AnCompletePointColor;
                }
                else if (currentScene == lvl)
                {
                    var image = Instantiate(_biomPointPrefab, _biomPointHolder).GetComponent<Image>();
                    image.color = _state.LevelsStorage.CyrrentPointColor;
                    image.transform.localScale = new Vector3(1, 1.2f, 1);
                }
                else
                {
                    var image = Instantiate(_biomPointPrefab, _biomPointHolder).GetComponent<Image>();
                    image.color = _state.LevelsStorage.CompletePointColor;
                }
            }
        }


        #endregion Initialization

        #region Play
        public void CLickButtonSpawnUnit(string value)
        {
            if (!CanUseBuyButton) return;
            if (_state.Coins >= 300)
            {
                ref var butEvent = ref _world.GetPool<UnitsSpawnEventComponent>().Add(_world.NewEntity());
                butEvent.TypeUnit = value;
                butEvent.IsReward = false;
                //CheckActiveColor();
            }
            else
            {
                //todo ADS reward units
                if (HoopslyIntegration.IsRewardedReady())
                {
                    AddHeroByReward(value);
                }
                else
                {
                    //todo f
                    OpenFailedPanel();
                }
            }

            if (value == "1melee")
            {
                if (isEncounter)
                    TurnOffUltimateMelee(true);
                ScaleAnimation(_gameButtons[0]);
            }
            if (value == "1range")
            {
                if (isEncounter)
                    TurnOffUltimateRange(true);
                ScaleAnimation(_gameButtons[1]);
            }
            if (_state.Saves.TutorialState == 1)
            {
                ref var tutorComp = ref _tutorialPool.Get(_state.EntityInterface);
                if (tutorComp.TutorialState == TutorialComponent.TutorialStates.PreExit)
                {
                    tutorComp.TutorialState = TutorialComponent.TutorialStates.Exit;
                }
            }
        }
        public void AddHeroByReward(string value)
        {
            ref var addHeroByADSComp = ref _addHeroByADSPool.Add(_world.NewEntity());
            Func<int> successFunc = () =>
            {
                ref var butEvent = ref _world.GetPool<UnitsSpawnEventComponent>().Add(_world.NewEntity());
                butEvent.TypeUnit = value;
                butEvent.IsReward = true;
                //CheckActiveColor();
                return 0;
            };
            Func<int> failedFunc = () =>
            {
                //todo f
                OpenFailedPanel();
                return 0;
            };
            addHeroByADSComp.SuccessFunc = successFunc;
            addHeroByADSComp.FailedFunc = failedFunc;
            addHeroByADSComp.PlayingLevel = _playingLevel;
        }
        public void ClickButtonPlay()
        {
            UltimateSprite(_state.GetHighLevelByType("Melee"), _state.GetHighLevelByType("Range"));
            if (!CanUsePlay) return;
            _playingLevel = true;
            _settingButtons[0].GetComponent<Button>().interactable = false;
            isStart = true;
            SettingsButtonMove();
            BiomPanelMove();
            var seq = DOTween.Sequence();
            seq.Append(_panelStart.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0), 0.3f, 5, 1));
            seq.Append(_panelStart.transform.DOMoveY(Screen.height * -0.1f, 1, false));
            
            /*string level_id = $"level_{_state.Saves.LVL}";
            bool measureFPS = true;
            string skin = "";
            string gameType = "";*/

            //HoopslyIntegration.RaiseLevelStartEvent(level_id, measureFPS, skin, gameType);

            var levelID = new Parameter("level_id", _state.Saves.LVL);

            FirebaseAnalytics.LogEvent("level_start", levelID);

            _panelGame.SetActive(true);
            if (_panelGame.activeSelf)
            {
                _ultimateMelee.transform.DOMoveX(Screen.width * 0.2f, 1, false);
                _ultimateRange.transform.DOMoveX(Screen.width * 0.8f, 1, false);
            }
            // _world.GetPool<EnablePlayLevelEvent>().Add(_state.EntityLevel);
            //_panelGO.SetActive(false);
            //_world.GetPool<EnablePlayLevelEvent>().Add(_state.EntityLevel);
            _world.GetPool<CameraBezierComponent>().Add(_state.EntityCamera);
            // ref var countdown = ref _world.GetPool<CountdownComponent>().Add(_world.NewEntity());
            // countdown.maxAmount = 60;
            // if (_state.Saves.Save.timerInter <= 0)
            // {
            //     countdown.currentAmount = countdown.maxAmount;
            // }
            // else
            // {
            //     countdown.currentAmount = _state.Saves.Save.timerInter;
            // }

            if (_state.Saves.TutorialState == 1)
            {
                ref var tutorComp = ref _tutorialPool.Get(_state.EntityInterface);
                if (tutorComp.TutorialState == TutorialComponent.TutorialStates.MinusThree)
                {
                    tutorComp.TutorialState = TutorialComponent.TutorialStates.Three;
                }
            }
        }
        public void Ultimate(string type)
        {
            if (!CanUseUltimate) return;
            if (type == "Range")
            {
                ref var ultimateComp = ref _ultimatePool.Add(_state.EntityRangeUltimate);
                ultimateComp.UltimateType = type;
                ScaleAnimation(_ultimateRange);
            }
            else if (type == "Melee")
            {
                ref var ultimateComp = ref _ultimatePool.Add(_state.EntityMeleeUltimate);
                ultimateComp.UltimateType = type;
                ScaleAnimation(_ultimateMelee);
            }
            if (_state.Saves.TutorialState == 1)
            {
                ref var tutorComp = ref _tutorialPool.Get(_state.EntityInterface);
                if (tutorComp.TutorialState == TutorialComponent.TutorialStates.MinusFive)
                {
                    tutorComp.TutorialState = TutorialComponent.TutorialStates.Five;
                }
            }
        }
        #endregion Play

        #region Changers
        public void ChangePointSprite(int index)
        {
            _pointsImage[index].sprite = _pointSprite;
        }
        public void KeysChanger()
        {
            _keysText.text = _state.KeysCount.ToString();
        }
        public void CoinsChanger(ulong value)
        {
            _textCoins.text = value.ToString();
        }
        public void RewardText(ulong Value)
        {
            _onlyRewardText.GetComponent<Text>().text = Value.ToString();
        }
        public void ChangeRewardedText(ulong value)
        {
            _rewardeText.text = value.ToString();
            _revardedValue = value;

        }
        private ulong _revardedValue = 0;
        public void GetRewardedCoin()
        {
            _state.Coins += _revardedValue;
            _state.Saves.SaveCoin(_state.Coins);
        }
        #endregion Changers

        #region ActivatioPanels
        public void ChangeLevelText(int value)
        {
            _levelText.text = "Level " + value.ToString();
        }
        public void BeforeStartPanel(bool value)
        {
            _beforeStartPanel.SetActive(value);
        }
        public void GamePanel(bool value)
        {
            _panelGame.SetActive(value);
        }
        public void LosePanel(bool value)
        {
            _panelLose.SetActive(value);
        }
        public void WinPanel(bool value)
        {
            _panelWin.SetActive(value);
        }
        public void ScorePanel(bool value)
        {
            _panelScore.SetActive(value);
        }
        public void ClickTest(string value)
        {
            Debug.Log(_state.GetHighLevelByType(value));
        }
        public void SettingsPanel()
        {
            _settignsPanel.transform.DOMoveY(Screen.height * 1.5f, 1, false);
        }
        public void SettingsPanelStart()
        {
            _settignsPanel.transform.DOMoveY(Screen.height * 1.11f, 0, false);
        }
        public void OpenFailedPanel()
        {
            //StopCoroutine(WaitAndCloseFailedPanel());
            _failedRewardPanel.SetActive(true);
            _gameButtons[10].GetComponent<Button>().interactable = false;
            Animator animator = _failedRewardPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isWin = animator.GetBool("isFailed");
                animator.SetBool("isFailed", true);
            }
            StartCoroutine(WaitAndCloseFailedPanel());
        }
        public IEnumerator WaitAndCloseFailedPanel()
        {            
            yield return new WaitForSeconds(2.5f);
            Animator animator = _failedRewardPanel.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isFailed", false);
                animator.SetBool("isFailedEnd", true);
            }
            StartCoroutine(WaitFailedRewardPanelClosed());
        }
        public IEnumerator WaitFailedRewardPanelClosed()
        {
            yield return new WaitForSeconds(1);
            _failedRewardPanel.SetActive(false);
            _gameButtons[10].GetComponent<Button>().interactable = true;
        }
        #endregion ActivationPanels

        #region Images
        public void UltimateSprite(string melee, string range)
        {
            if (melee != "empty" && melee != string.Empty)
            {
                _ultimateMelee.GetComponent<Image>().sprite = _state.UltimateStorage.GetSpriteByID(melee);
                _rankUltimates[0].sprite = _state.UltimateStorage.GetRankByID(melee);
                //_ultimateMelee.GetComponent<Image>().color = _state.UltimateStorage.GetColorByID(melee);
            }
            if (range != "empty" && range != string.Empty)
            {
                _ultimateRange.GetComponent<Image>().sprite = _state.UltimateStorage.GetSpriteByID(range);
                _rankUltimates[1].sprite = _state.UltimateStorage.GetRankByID(range);
                //_ultimateRange.GetComponent<Image>().color = _state.UltimateStorage.GetColorByID(range);
            }
        }
        private bool _heroOffer = false;
        private void AddHeroOffer()
        {
            if (_heroOffer == false)
            {
                //логика разделения офферов
                //ref var offerComp = ref _addOfferPool.Add(_world.NewEntity()); old
                if (_playingLevel)
                {
                    HoopslyIntegration.RaiseAdOfferEvent(AdRewardType.hero);
                    //offerComp.AdRewardTypes = Advertising.AdRewardTypes.hero; old
                }
                else
                {
                    HoopslyIntegration.RaiseAdOfferEvent(AdRewardType.preLevelHero);
                    //offerComp.AdRewardTypes = Advertising.AdRewardTypes.preLevelHero;  old
                }
                _heroOffer = true;
            }
        }
        public void CheckActiveColor()
        {
            int slotEmpty = _state.GetEmptyPlayerUnitsIndex();
            if (slotEmpty == -1)
            {
                _gameButtons[0].gameObject.GetComponent<Button>().interactable = false;
                _gameButtons[1].gameObject.GetComponent<Button>().interactable = false;
                if (_state.Coins >= 300)
                {
                    _gameButtons[0].transform.GetChild(1).gameObject.SetActive(true);
                    _gameButtons[1].transform.GetChild(1).gameObject.SetActive(true);
                    _gameButtons[12].gameObject.SetActive(false);
                    _gameButtons[13].gameObject.SetActive(false);
                    _heroOffer = false;
                    for (int i = 0; i < _freeButtons.Length; i++)
                    {
                        _freeButtons[i].SetActive(false);
                    }
                }
                else
                {
                    AddHeroOffer();
                    _gameButtons[0].transform.GetChild(1).gameObject.SetActive(false);
                    _gameButtons[1].transform.GetChild(1).gameObject.SetActive(false);
                    _gameButtons[12].gameObject.SetActive(true);
                    _gameButtons[13].gameObject.SetActive(true);
                    for (int i = 0; i < _freeButtons.Length; i++)
                    {
                        _freeButtons[i].SetActive(true);
                    }
                }
            }
            else
            {
                _gameButtons[0].gameObject.GetComponent<Button>().interactable = true;
                _gameButtons[1].gameObject.GetComponent<Button>().interactable = true;
                if (_state.Coins >= 300)
                {
                    _gameButtons[0].transform.GetChild(1).gameObject.SetActive(true);
                    _gameButtons[1].transform.GetChild(1).gameObject.SetActive(true);
                    _gameButtons[12].gameObject.SetActive(false);
                    _gameButtons[13].gameObject.SetActive(false);
                    _gameButtons[0].gameObject.GetComponent<Button>().interactable = true;
                    _gameButtons[1].gameObject.GetComponent<Button>().interactable = true;
                    _heroOffer = false;
                    for (int i = 0; i < _freeButtons.Length; i++)
                    {
                        _freeButtons[i].SetActive(false);
                    }
                }
                else
                {
                    AddHeroOffer();
                    _gameButtons[0].transform.GetChild(1).gameObject.SetActive(false);
                    _gameButtons[1].transform.GetChild(1).gameObject.SetActive(false);
                    _gameButtons[12].gameObject.SetActive(true);
                    _gameButtons[13].gameObject.SetActive(true);
                    for (int i = 0; i < _freeButtons.Length; i++)
                    {
                        _freeButtons[i].SetActive(true);
                    }
                }
            }
        }
        public void CooldownAnimationRange(float value) //�������� ���������� ������ �� �����������
        {
            _ultimateRange.GetComponent<Button>().interactable = false;
            ImageCooldown[0].fillAmount -= 1.0f / value * Time.deltaTime;
            if (ImageCooldown[0].fillAmount == 0 && isEncounter)
                _ultimateRange.GetComponent<Button>().interactable = true;
        }
        public void CooldownAnimationMelee(float value)
        {
            _ultimateMelee.GetComponent<Button>().interactable = false;
            ImageCooldown[1].fillAmount -= 1.0f / value * Time.deltaTime;
            if (ImageCooldown[1].fillAmount == 0 && isEncounter)
                _ultimateMelee.GetComponent<Button>().interactable = true;
        }
        public void TurnOffUltimateMelee(bool value)
        {
            var cd = ImageCooldown[1].fillAmount;
            if (!value)
            {
                ImageCooldown[3].fillAmount = 1;
                //ImageCooldown[1].gameObject.SetActive(value);
                _ultimateMelee.GetComponent<Button>().interactable = value;
            }
            else if (value)
            {
                _ultimateMelee.GetComponent<Button>().interactable = value;
                ImageCooldown[1].gameObject.SetActive(value);
                ImageCooldown[3].fillAmount = 0;
            }
        }
        public void TurnOffUltimateRange(bool value)
        {
            if (!value)
            {
                ImageCooldown[2].fillAmount = 1;
                //ImageCooldown[0].gameObject.SetActive(value);
                _ultimateRange.GetComponent<Button>().interactable = value;
            }
            else if (value)
            {
                _ultimateRange.GetComponent<Button>().interactable = value;
                ImageCooldown[0].gameObject.SetActive(value);
                ImageCooldown[2].fillAmount = 0;
            }
        }
        public void ChangeSundukImage(int index, Sprite sprite, bool addHero, ulong money)//ulong money
        {
            _sundukImages[index].gameObject.SetActive(true);
            _sundukImages[index].sprite = sprite;
            _rewards[index].transform.GetChild(2).transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            var im = _rewards[index].GetComponent<Image>();
            im.enabled = false;
            if(addHero)
            {
                _rewards[index].transform.GetChild(1).transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
            else
            {
                _sundukText[index].gameObject.SetActive(true);
                _sundukText[index].text = money.ToString();
            }
        }
        public void PlaySundukParticleIfOpenHero(int index)
        {

        }
        public void SetActiveRewardHolder(bool value)
        {
            _rewardHolder.SetActive(value);   
        }
        #endregion Images

        #region ClickEvents
        public void NextButtonEvent()
        {
            ScaleAnimation(_gameButtons[9]);
            _panelNewHero.SetActive(!_panelNewHero.activeSelf);
        }
        public void ClickSundukButton(int index)
        {
            ref var openSundukComp = ref _openSundukPool.Add(_world.NewEntity());
            openSundukComp.Index = index;
            _rewards[index].GetComponent<Button>().interactable = false;
            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i].GetComponent<Image>().color != Color.black)
                {
                    _keys[i].GetComponent<Image>().color = Color.black;
                    break;
                }
            }
            _currentKeys--;
            _oppenedChest++;
            if (_currentKeys == 0)
            {
                for (int i = 0; i < _rewards.Length; i++)
                {
                    _rewards[i].GetComponent<Button>().interactable = false;
                }
                if (_oppenedChest < 9)
                {
                    isRewardEnd = false;
                    ActiveRewardKeysButton();
                    StartCoroutine(WaitToCancel());
                }
                else if (_oppenedChest == 9)
                {
                    isRewardEnd = true;
                    _adsButton.SetActive(false);
                    _cancelAdsChestAndKeys.SetActive(false);
                    StopCoroutine(WaitToCancel());
                    _nextLevel.gameObject.SetActive(true);
                }
                else if (_currentKeys <= 2)
                {
                    isRewardEnd = true;
                    _adsButton.SetActive(false);
                    _cancelAdsChestAndKeys.SetActive(false);
                    _nextLevel.gameObject.SetActive(true);
                }
            }
            KeysChanger();
        }
        private void ActiveRewardKeysButton()
        {
            Debug.Log("OFFFFFFFFFFFFFFFFFFFFFERKEY");
            _adsButton.SetActive(true);
            HoopslyIntegration.RaiseAdOfferEvent(AdRewardType.keys);
            //ref var offerComp = ref _addOfferPool.Add(_world.NewEntity()); old
            //offerComp.AdRewardTypes = Advertising.AdRewardTypes.keys; old

        }
        public void ReloadScene()
        {
            ScaleAnimation(_gameButtons[3]);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadNextLevel()
        {
            _failedRewardPanel.SetActive(false);
            int index = 0;
            if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1)
            {
                index = 1;
            }
            else
            {
                index = SceneManager.GetActiveScene().buildIndex + 1;
            }
            _state.Saves.SaveSceneNumber(index);
            _state.Saves.SaveLevel(_state.Saves.LVL + 1);
            SceneManager.LoadScene(index);
        }
        public void AdsChest()
        {
            //todo ADS reward keys
            ref var vibrationComp = ref _vibrationPool.Add(_world.NewEntity());
            vibrationComp.Vibration = VibrationEvent.VibrationType.MediumImpact;

            if (HoopslyIntegration.IsRewardedReady())
            {
                ref var addKeyComp = ref _addKeysByADSPool.Add(_world.NewEntity());

                Func<int> successFunc = () =>
                {
                    _currentKeys = 3;
                    for (int i = 0; i < _keys.Length; i++)
                    {
                        if (_keys[i].GetComponent<Image>().color == Color.black)
                        {
                            _keys[i].GetComponent<Image>().color = Color.white;
                        }
                    }
                    for (int i = 0; i < _rewards.Length; i++)
                    {
                        _rewards[i].GetComponent<Button>().interactable = true;
                    }
                    KeysChanger();
                    _adsButton.SetActive(false);
                    StopCoroutine(WaitToCancel());
                    _cancelAdsChestAndKeys.SetActive(false);
                    return 0;
                };
                Func<int> failedFunc = () =>
                {
                    //todo f
                    OpenFailedPanel();
                    return 0;
                };
                addKeyComp.SuccessFunc = successFunc;
                addKeyComp.FailedFunc = failedFunc;

            }
            else
            {
                //todo f
                OpenFailedPanel();
            }
        }
        public void StopRotate() //при нажатии на кнопку стоп запускается система, которая останавливает барабан и вызывает событие через коллайдер для получения награды
        {
            ref var vibrationComp = ref _vibrationPool.Add(_world.NewEntity());
            vibrationComp.Vibration = VibrationEvent.VibrationType.MediumImpact;

            if (HoopslyIntegration.IsRewardedReady())
            {
                _spinStopPool.Add(_world.NewEntity());
                _spinPool.Del(_state.EntityInterface);
                _gameButtons[10].gameObject.SetActive(false);
                _cancelAdsWinEvent.gameObject.SetActive(false);
                if (_state.Saves.LVL >= 4)
                    _state.Saves.SaveTimer(60);
                isSpining = false;
            }
            else
            {
                //todo f
                OpenFailedPanel();
            }
        }
        public void CancelAdsWinEvent()
        {
            ScaleAnimation(_cancelAdsWinEvent);
            _spinObject.transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;
            _spinPool.Del(_state.EntityInterface);
            _gameButtons[10].SetActive(false);
            _failedRewardPanel.SetActive(false);
            ChangeRewardedText(_rewardPool.Get(_state.EntityLevelReward).Value);
            GetRewardedCoin();
            CoinsChanger(_state.Saves.AllCoin);
            PlayParticleCoins();
            EndLevelAds();
            //RewardInfo();
            _cancelAdsWinEvent.SetActive(false);
            Debug.Log("CancelAds");
        }
        #endregion ClickEvents

        #region PlayAnimation
        public void PlayParticleCoins()
        {
            _particleSystems[0].Play();
        }
        public void ScaleAnimation(GameObject target)
        {
            target.transform.DOPunchScale(new Vector3(-0.3f, -0.3f, 0), 0.3f, 7, 1);
            target.transform.DOScale(1, 1);
        }
        public void BiomPanelMove()
        {
            _gameButtons[8].transform.DOMoveY(Screen.height * 1.25f, 1, false);
        }
        #endregion PlayAnimation

        #region Coroutines
        private IEnumerator WaitToCancelAds()
        {
            yield return new WaitForSeconds(2.5f);
            if (isSpining)
                _cancelAdsWinEvent.SetActive(true);
        }

        public IEnumerator WaitAndShowPanel(float time, bool value, bool isWin)
        {
            yield return new WaitForSeconds(time);
            if (isWin) WinEvent(value);
            else LoseEvent(value);
        }
        private IEnumerator WaitToCancel()
        {
            yield return new WaitForSeconds(3);
            _cancelAdsChestAndKeys.SetActive(true);
            _EXITBUTTON.SetActive(true);
                
        }
        private IEnumerator WaitInvokeChest()
        {
            yield return new WaitForSeconds(1);
            InvokeChestAndKeysPanel();
        }
        public IEnumerator WaitRewardInfo()
        {
            //todo f
            _failedRewardPanel.SetActive(false);
            _spinObject.SetActive(!_spinObject.activeSelf);
            _rewardedPanel.GetComponent<Image>().enabled = true;
            _reward.SetActive(true);
            yield return new WaitForSeconds(1);
            RewardInfo();
        }
        public IEnumerator WaitRewardButton()
        {
            yield return new WaitForSeconds(3);
            Debug.Log("Reward!!!");
            _gameButtons[10].SetActive(true);
        }
        #endregion Coroutines

        #region Event
        public void LoseEvent(bool value)
        {
            LosePanel(value);
            Animator animator = _panelLose.GetComponent<Animator>();
            if (animator != null)
            {
                bool isLose = animator.GetBool("isLose");
                //animator.SetBool("isLose", true);
                //StartCoroutine(WaitAndShowInter());
                _gameButtons[3].SetActive(true);
            }
        }
        //public IEnumerator WaitAndShowInter()
        //{
        //    yield return new WaitForSeconds(1.5f);
        //    LoseLevelInterstitial();
        //}
        public void WinEvent(bool value)
        {
            isSpining = true;
            WinPanel(value);
            Animator animator = _panelWin.GetComponent<Animator>();
            StartRotate(); 
            //StartCoroutine(WaitToCancelAds());
            if (animator != null)
            {
                bool isWin = animator.GetBool("isWin");
                animator.SetBool("isWin", true);
            }
        }
        public void StartWait(float time, bool value, bool isWin)
        {
            StartCoroutine(WaitAndShowPanel(time, value, isWin));
        }
        public void StartRotate() //при нажатии на кнопку ревард запускается система, которая крутит барабан
        {
            _spinPool.Add(_state.EntityInterface);
            _spinObject.transform.GetChild(2).gameObject.GetComponent<BoxCollider>().enabled = true;
            HoopslyIntegration.RaiseAdOfferEvent(AdRewardType.multiply);

            //ref var offerComp = ref _addOfferPool.Add(_world.NewEntity());
            //offerComp.AdRewardTypes = Advertising.AdRewardTypes.multiply;

            _gameButtons[10].SetActive(true);
            StartCoroutine(WaitToCancelAds());
            //StartCoroutine(WaitRewardButton());
            isSpining = true;
        }
        public void InvokeChestAndKeysPanel()
        {
            _currentKeys = 3;
            _oppenedChest = 0;
            Animator animator = _sundukPanel.GetComponent<Animator>();
            _sundukPanel.SetActive(!_sundukPanel.activeSelf);
            _spinObject.SetActive(!_spinObject.activeSelf);
            _panelWin.SetActive(!_panelWin.activeSelf);
            if (animator != null)
            {
                bool isKeys = animator.GetBool("isKeys");
                animator.SetBool("isKeys", true);
            }
        }
        public void RewardInfo()
        {
            if (_state.KeysCount >= 3 && _state.Saves.LVL >= 3)
                StartCoroutine(WaitInvokeChest());
            else
            {
                LoadNextLevel();
            }
        }
        public void NewHeroPanel(string value)
        {
            if (!isTutorial)
            {
                _damageInfo.text = _state.UnitStorage.GetDamageByID(value).ToString();
                _healthInfo.text = _state.UnitStorage.GetHealthByID(value).ToString();
                _nameInfo.text = _state.UnitStorage.GetNameByID(value);
                _newHeroImage.sprite = _state.UnitStorage.GetSpriteByID(value);
                _panelNewHero.SetActive(!_panelNewHero.activeSelf);
            }
        }
        //вин интер
        public void EndLevelAds()
        {
            //todo ADS interDone
            if (_state.Saves.LVL >= 4)
            {
                if (HoopslyIntegration.IsInterstitialReady() && _state.Saves.timerInter == 0)
                {
                    ref var interstitialComp = ref _interstitialPool.Add(_world.NewEntity());
                    Func<int> func = () =>
                    {
                        //запустить сундуки
                        StartCoroutine(WaitRewardInfo());
                        ref var timerComp = ref _world.GetPool<CountdownComponent>().Add(_world.NewEntity());
                        timerComp.currentAmount = 60;

                        return 0;
                    };
                    interstitialComp.Func = func;
                }
                else
                {
                    //запустить сундуки
                    StartCoroutine(WaitRewardInfo());
                }
            }
            else
            {
                StartCoroutine(WaitRewardInfo());
            }
        }
        //интер после луза
        public void LoseLevelInterstitial()
        {
            if (HoopslyIntegration.IsInterstitialReady() && _state.Saves.timerInter == 0)
            {
                ref var interstitialComp = ref _interstitialPool.Add(_world.NewEntity());
                Func<int> func = () =>
                {
                    _gameButtons[3].SetActive(true);
                    ref var timerComp = ref _world.GetPool<CountdownComponent>().Add(_world.NewEntity());
                    timerComp.currentAmount = 60;
                    return 0;
                };
                interstitialComp.Func = func;
            }
            else
            {

            }
            _gameButtons[3].SetActive(true);
        }
        public void IsRewardMultiply(ulong addedCoins)
        {
            //todo ADS
            ref var addMultiplyComp = ref _multiplyCoinsByADSPool.Add(_world.NewEntity());
            addMultiplyComp.AddedCoin = addedCoins;
            Func<int> successFunc = () =>
            {
                
                _state.Coins += addedCoins;
                _state.Saves.SaveCoin(_state.Coins);
                CoinsChanger(_state.Saves.AllCoin);
                Debug.Log("Canvas " + addedCoins);
                StartCoroutine(WaitRewardInfo());
                PlayParticleCoins();
                return 0;
            };
            Func<int> failedFunc = () =>
            {
                //todo f
                OpenFailedPanel();
                StartCoroutine(WaitRewardInfo());
                return 0;
            };
            addMultiplyComp.FailedFunc = failedFunc;
            addMultiplyComp.SuccessFunc = successFunc;
        }
        
        #endregion Event

        #region Settings
        public void SettingsButtonMove()
        {
            var _sequence = DOTween.Sequence();
            _settingButtons[4].SetActive(!_settingButtons[4].activeSelf);
            _settingButtons[5].SetActive(!_settingButtons[5].activeSelf);
            if (isStart)
                _sequence.Append(_settignsPanel.transform.DOMoveY(Screen.height * 1.5f, 1, false));
            else if (!_settingButtons[4].activeSelf)
            {
                _sequence.Append(_settignsPanel.transform.DOMoveY(Screen.height * 0.82f, 1, false));
            }
            else if (!_settingButtons[5].activeSelf)
            {
                _sequence.Append(_settignsPanel.transform.DOMoveY(Screen.height * 1.11f, 1, false));
            }
            CheckVolumeMusicVibration();
        }
        public void VolumeButtonEffect()
        {
            _settingButtons[6].SetActive(!_settingButtons[6].activeSelf);
            _settingButtons[7].SetActive(!_settingButtons[7].activeSelf);
            if (_state.Saves.Sounds == 1)
            {
                _settingButtons[2].GetComponent<Button>().targetGraphic = _settingButtons[7].GetComponent<Image>();
                _state.Saves.SaveSounds(0);
            }
            else if (_state.Saves.Sounds == 0)
            {
                _settingButtons[2].GetComponent<Button>().targetGraphic = _settingButtons[6].GetComponent<Image>();
                _state.Saves.SaveSounds(1);
            }
        }
        public void MusicButtonEffect()
        {
            _settingButtons[8].SetActive(!_settingButtons[8].activeSelf);
            _settingButtons[9].SetActive(!_settingButtons[9].activeSelf);
            if (_state.Saves.Music == 1)
            {
                _settingButtons[3].GetComponent<Button>().targetGraphic = _settingButtons[9].GetComponent<Image>();
                _state.Saves.SaveMusic(0);
            }
            else if (_state.Saves.Music == 0)
            {
                _settingButtons[3].GetComponent<Button>().targetGraphic = _settingButtons[8].GetComponent<Image>();
                _state.Saves.SaveMusic(1);
            }
        }
        public void VibrationButtonEffect()
        {
            _settingButtons[10].SetActive(!_settingButtons[10].activeSelf);
            _settingButtons[11].SetActive(!_settingButtons[11].activeSelf);
            if (_state.Saves.Vibration == 1)
            {
                _settingButtons[1].GetComponent<Button>().targetGraphic = _settingButtons[11].GetComponent<Image>();
                _state.Saves.SaveVibration(0);
            }

            else if (_state.Saves.Vibration == 0)
            {
                _settingButtons[1].GetComponent<Button>().targetGraphic = _settingButtons[10].GetComponent<Image>();
                _state.Saves.SaveVibration(1);
            }
        }
        public void CheckVolumeMusicVibration()
        {
            if (_state.Saves.Vibration == 1)
            {
                _settingButtons[1].GetComponent<Button>().targetGraphic = _settingButtons[10].GetComponent<Image>();
                _settingButtons[10].SetActive(true);
                _settingButtons[11].SetActive(false);
            }
            else if (_state.Saves.Vibration == 0)
            {
                _settingButtons[1].GetComponent<Button>().targetGraphic = _settingButtons[11].GetComponent<Image>();
                _settingButtons[11].SetActive(true);
                _settingButtons[10].SetActive(false);
            }
            if (_state.Saves.Sounds == 1)
            {
                _settingButtons[2].GetComponent<Button>().targetGraphic = _settingButtons[6].GetComponent<Image>();
                _settingButtons[6].SetActive(true);
                _settingButtons[7].SetActive(false);
            }
            else if (_state.Saves.Sounds == 0)
            {
                _settingButtons[2].GetComponent<Button>().targetGraphic = _settingButtons[7].GetComponent<Image>();
                _settingButtons[7].SetActive(true);
                _settingButtons[6].SetActive(false);
            }
            if (_state.Saves.Music == 1)
            {
                _settingButtons[3].GetComponent<Button>().targetGraphic = _settingButtons[8].GetComponent<Image>();
                _settingButtons[8].SetActive(true);
                _settingButtons[9].SetActive(false);
            }
            else if (_state.Saves.Music == 0)
            {
                _settingButtons[3].GetComponent<Button>().targetGraphic = _settingButtons[9].GetComponent<Image>();
                _settingButtons[9].SetActive(true);
                _settingButtons[8].SetActive(false);
            }
        }
        #endregion Settings

        #region Others
        public void EncounterCheck(bool value)
        {
            isEncounter = value;
        }
        public void TutorialHeroPanel()
        {
            _tutorialHeroPanel.SetActive(!_tutorialHeroPanel.activeSelf);
        }
        public GameObject GetSpin()
        {
            return _spinObject;
        }
        #endregion Others

    }
}