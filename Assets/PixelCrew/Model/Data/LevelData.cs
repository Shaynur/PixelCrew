﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.PixelCrew.Model.Definitions.Player;
using UnityEngine;

namespace Assets.PixelCrew.Model.Data {

    public class LevelData {

        [SerializeField] private List<LevelProgress> _progress = new List<LevelProgress>();

        public int GetLevel(StatId id) {

            foreach (var levelProgress in _progress) {
                if (levelProgress.Id == id)
                    return levelProgress.Level;
            }
            return 0;
        }

        public void LevelUp(StatId id) {
            var progress = _progress.FirstOrDefault(x => x.Id == id);
            if (progress == null)
                _progress.Add(new LevelProgress(id, 1));
            else
                progress.Level++;

        }
    }

    [Serializable]
    public class LevelProgress {
        public StatId Id;
        public int Level;

        public LevelProgress(StatId id, int level) {
            Id = id;
            Level = level;
        }
    }
}
