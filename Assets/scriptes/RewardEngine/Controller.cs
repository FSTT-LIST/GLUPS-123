using UnityEngine;

// the test controller of our library
namespace com.glups.Reward
{
    public class Controller 
    {

        internal RewardModel _model;
        internal RewardStrategy _FRS, _VRS, _VIS, _FIS;
        internal RewardStrategy _currentStrategy;
        internal View _view;

        public Controller(RewardModel model, View view)
        {
            _model = model;
            StrategyParameters parameters = new StrategyParameters();
            _FRS = new FixedRatioSchedule(_model, parameters);
            _FIS = new FixedIntervalSchedule(_model, parameters);
            _VRS = new VariableRatioSchedule(_model, parameters);
            _VIS = new VariableIntervalSchedule(_model, parameters);

            _view = view;
            _currentStrategy = null;
        }

        internal virtual int Score
        {
            get
            {
                return _model._score;
            }
        }
        internal virtual int RewardScore
        {
            get
            {
                return _model._iScore;
            }
        }

        internal virtual void openLevel(int level)
        {
            _model.openLevel(level);

            string playerName = PlayerPrefs.GetString("name:" + PlayerPrefs.GetString("Player"));
            char firstLetter = playerName[0];
            int asciiCode = (int)firstLetter;
            
            if (asciiCode % 2 == 0)
            {
                if (level == 1) { _currentStrategy = _FRS; }
                else if(level == 2 ) { _currentStrategy = _FIS; }
                else if (level == 3) { _currentStrategy = _VRS; }
                else if (level == 4) { _currentStrategy = _VIS; }
                else if (level == 0) { _currentStrategy = _FRS; }
                else { _currentStrategy = null; }
            }
            else if(asciiCode % 2 == 1)
            {
                if (level == 1) { _currentStrategy = _VRS; }
                else if (level == 2) { _currentStrategy = _VIS; }
                else if (level == 3) { _currentStrategy = _FRS; }
                else if (level == 4) { _currentStrategy = _FIS; }
                else if (level == 0) { _currentStrategy = _FRS; }
                else { _currentStrategy = null; }
            }

            if (_currentStrategy != null) { _currentStrategy.updateRank(); }
        }

        internal virtual void traceModel(int date, string playerID)
        {
            string strategyName = "?";

            if (_currentStrategy != null)
            {
                strategyName = _currentStrategy.name();
            }
            _model.trace(date, playerID, strategyName);
        }

        internal virtual void addPositive(int nsuccess)
        {
            if (_currentStrategy == null)
            {
                Debug.Log("Controller: must select a strategy before calling addPositive");
                return;
            }
            _currentStrategy.addPositive(nsuccess); // this returns the iScore
                                                    //_view.updatePositiveScore(_model.getScore(), _model.getRewardScore());

            int rank = _currentStrategy.updateRank();
            if (rank > 0)
            {
                _view.updateRank(rank);
            }

            int reward = _currentStrategy.updateReward();
            if (reward > 0)
            {
                _view.updateReward(reward);
            }
        }

        internal virtual void addNegative(int nfailure)
        {
            if (_currentStrategy == null)
            {
                Debug.Log("Controller: must select a strategy before calling addNegative");
                return;
            }
            _currentStrategy.addNegative(nfailure); // this returns the iScore
                                                    //_view.updateNegativeScore(_model.getScore(), _model.getRewardScore());

            int rank = _currentStrategy.updateRank();
            if (rank > 0)
            {
                _view.updateRank(rank);
            }

            int reward = _currentStrategy.updateReward();
            if (reward > 0)
            {
                _view.updateReward(reward);
            }
        }
    }
}