using System;
using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class StoveCounter : Counter
    {
        private enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }

        [SerializeField] private FryingRecipe[] fryingRecipes;
        [SerializeField] private BurningRecipe[] burningRecipes;

        private State _state;
        private float _fryingTimer;
        private FryingRecipe _currentFryingRecipe;
        private float _burningTimer;
        private BurningRecipe _currentBurningRecipe;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            if (!HasKitchenObject()) return;

            switch (_state)
            {
                case State.Idle: break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    if (_fryingTimer < _currentFryingRecipe.FryingMaxTime) return;

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentFryingRecipe.Output, this);
                    _burningTimer = 0f;

                    _currentBurningRecipe = GetBurningRecipeWithInput(GetKitchenObject().Data);

                    _state = State.Fried;
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    if (_burningTimer < _currentBurningRecipe.BurningMaxTime) return;

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentBurningRecipe.Output, this);
                    _burningTimer = 0f;
                    _state = State.Burned;
                    break;
                case State.Burned: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override void Interact(Player player)
        {
            if (HasKitchenObject())
            {
                if (!player.HasKitchenObject())
                    _state = State.Idle;
                GetKitchenObject().SetAndTeleportToParent(player);
            }
            else if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject()))
            {
                player.GetKitchenObject().SetAndTeleportToParent(this);
                _currentFryingRecipe = GetFryingRecipeWithInput(GetKitchenObject().Data);
                _state = State.Frying;
                _fryingTimer = 0f;
            }

            bool HasRecipe(KitchenObject input)
            {
                return fryingRecipes.Any(recipe => recipe.Input == input.Data);
            }
        }

        /// <summary>
        /// Returns the frying recipe for the given input, or null if there is none
        /// </summary>
        /// <param name="input">If it is referenced as input in a recipe, returns the recipe.
        /// Null if there is no match</param>
        private FryingRecipe GetFryingRecipeWithInput(KitchenObjectData input)
        {
            // Select the possible output of the cutting recipe
            var slicedCandidates = from cuttingRecipe in fryingRecipes
                where cuttingRecipe.Input == input
                select cuttingRecipe;
            // Get the first possible output, or null if there is none
            return slicedCandidates.FirstOrDefault();
        }

        /// <summary>
        /// Returns the burning recipe for the given input, or null if there is none
        /// </summary>
        /// <param name="input">If it is referenced as input in a recipe, returns the recipe.
        /// Null if there is no match</param>
        private BurningRecipe GetBurningRecipeWithInput(KitchenObjectData input)
        {
            // Select the possible output of the cutting recipe
            var slicedCandidates = from cuttingRecipe in burningRecipes
                where cuttingRecipe.Input == input
                select cuttingRecipe;
            // Get the first possible output, or null if there is none
            return slicedCandidates.FirstOrDefault();
        }
    }
}
