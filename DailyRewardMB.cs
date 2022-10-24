using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace Client 
{
    public class DailyRewardMB : MonoBehaviour
    {
        private EcsPool<DailyRewardComponent> _dailyPool;
        [SerializeField] private Text _status;
        [SerializeField] private Transform _holder;
        [SerializeField] private RewardMB _reward;
        [SerializeField] private Button _claim;
        [SerializeField] private Sprite _money;
        [SerializeField] private Sprite _heroMelee;
        [SerializeField] private Sprite _heroRange;
        [SerializeField] private Transform _yourRewardHolder; 
        [SerializeField] private Image _yourReward;
        [SerializeField] private Text _yourRewardAmount;
        [SerializeField] private GameObject _dailyRewardFill;
        [SerializeField] private Text _alwaysInfo;
        [SerializeField] private GameObject _rainbowEffects;

        private int currentHeroReward
        {
            get => PlayerPrefs.GetInt("currentHeroReward", 0);
            set => PlayerPrefs.SetInt("currentHeroReward", value);
        }
        private int currentStreak
        {
            get => PlayerPrefs.GetInt("currentStreak", 0);
            set => PlayerPrefs.SetInt("currentStreak", value);
        }
        public DateTime? lastClaimTime
        {
            get 
            {
                string data = PlayerPrefs.GetString("lastClaimedTime", null);
                if(!string.IsNullOrEmpty(data))
                    return DateTime.Parse(data);
                return null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString("lastClaimedTime", value.ToString());
                else
                    PlayerPrefs.DeleteKey("lastClaimedTime");
            }
        }

        private bool isClaim;
        private int maxStreakCount = 8;
        private float claimCooldown = 24f/* / 24 / 60 / 6 / 2*/;
        private float claimDeadline = 48f;
        private int randomHero;
        private int money = 300;

        private List<RewardMB> rewards = new List<RewardMB>(); 

        private EcsWorld _world = default;
        private GameState _state;
        public void Init(EcsWorld world, GameState state)
        {
            _world = world;
            _state = state;
            _dailyPool = _world.GetPool<DailyRewardComponent>();
            CreateRewards();
            StartCoroutine(RewardStatsUpdater());
        }

        public void CreateRewards()
        {
            for (int i = 0; i < 8; i++)
            {
                var reward = Instantiate(_state.InterfaceConfig.RewardDaily, _holder);
                var rewardInfo = reward.GetComponent<RewardMB>();
                if(i == 0)
                    rewardInfo.SetReward(i, currentStreak, _money, money.ToString());
                else if (i < 7)
                {
                    rewardInfo.SetReward(i, currentStreak, _money, (money * 2 * i).ToString());
                }
                else if (i == 7)
                {
                    if (currentHeroReward == 0)
                    {
                        rewardInfo.SetReward(i, currentStreak, _heroMelee, "Hero!");
                    }
                    else if (currentHeroReward == 1)
                    {
                        rewardInfo.SetReward(i, currentStreak, _heroRange, "Hero!");
                    }
                }
                rewards.Add(rewardInfo);
            }
        }
        private IEnumerator RewardStatsUpdater()
        {
            while (true)
            {
                UpdateRewardState();
                yield return new WaitForSeconds(1);
            }
        }
        private void UpdateRewardState()
        {
            isClaim = true;
            if (lastClaimTime.HasValue)
            {
                var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

                //if (timeSpan.TotalHours > claimDeadline)
                //{
                //    lastClaimTime = null;
                //    currentStreak = 0;
                //}
                if (timeSpan.TotalHours < claimCooldown)
                {
                    isClaim = false;
                }
            }

            UpdateRewardsUI();
        }

        private void UpdateRewardsUI()
        {
            _claim.interactable = isClaim;
            if (isClaim)
            {
                _status.text = "Claim your reward!";
                _alwaysInfo.text = "Claim your reward!";
                _rainbowEffects.SetActive(true);
            }
            else
            {
                var nextClaimTime = lastClaimTime.Value.AddHours(claimCooldown);
                var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

                string cooldown = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
                _status.text = $"Come back in {cooldown}";
                _alwaysInfo.text = cooldown;
                _rainbowEffects.SetActive(false);
            }
        }

        public void Claim()
        {
            GameState.isDailyReward = true;
            ref var dailyComp = ref _dailyPool.Add(_world.NewEntity());
            if (currentStreak == 7)
            {
                dailyComp.isHero = true;
                if (currentHeroReward == 0)
                {
                    dailyComp.levelHero = "10melee";
                    dailyComp.typeHero = "Melee";
                    _yourReward.sprite = _heroMelee;

                    currentHeroReward = 1;
                }
                else
                {
                    dailyComp.levelHero = "10range";
                    dailyComp.typeHero = "Range";
                    _yourReward.sprite = _heroRange;

                    currentHeroReward = 0;
                }
                _yourRewardAmount.text = "New Hero!";
            }
            else
            {
                dailyComp.isHero = false;
                if (currentStreak == 0)
                {
                    dailyComp.money = money;
                }
                else
                {
                    dailyComp.money = money * 2 * currentStreak;
                }
                _yourReward.sprite = _money;
                _yourRewardAmount.text = dailyComp.money.ToString();
            }
            _yourRewardHolder.transform.DOScale(1f, 0.5f).OnComplete(()=>StartCoroutine(CloseTimer()));
            lastClaimTime = DateTime.UtcNow;
            currentStreak = (currentStreak + 1) % maxStreakCount;
        }
        private IEnumerator CloseTimer()
        {
            _dailyRewardFill.gameObject.SetActive(true);
            _yourRewardHolder.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            Close();
        }
        private void Close()
        {
            _dailyRewardFill.gameObject.SetActive(false);
            _yourRewardHolder.transform.DOScale(0f, 0.5f).OnComplete(()=>ClosePanel());
        }
        public void ClosePanel()
        {
            _yourRewardHolder.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.GetComponent<Image>().enabled = false;
            GameState.isDailyReward = true;
            this.transform.DOScale(0f, 0.5f).OnComplete(()=>OffPanel());
        }
        private void OffPanel()
        {
            for (int i = 0; i < _holder.childCount; i++)
            {
                Destroy(_holder.GetChild(i).gameObject);
            }
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        private void OnPanel()
        {
            this.gameObject.GetComponent<Image>().enabled = true;
        }
        public void OpenPanel() 
        {
            for (int i = 0; i < _holder.childCount; i++)
            {
                Destroy(_holder.GetChild(i).gameObject);
            }
            CreateRewards();
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.DOScale(1f, 0.5f).OnComplete(()=>OnPanel());
        }
    }
}
