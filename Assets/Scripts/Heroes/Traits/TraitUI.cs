using TMPro;
using UnityEngine;

namespace EG.Tower.Heroes.Traits
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _descriptionLabel;
        [SerializeField] private bool _showName = true;

        private void Awake()
        {
            _nameLabel.gameObject.SetActive(_showName);
        }

        public void Init(TraitData data)
        {
            _nameLabel.text = data.Name;
            _descriptionLabel.text = data.Description;
        }
    }
}
