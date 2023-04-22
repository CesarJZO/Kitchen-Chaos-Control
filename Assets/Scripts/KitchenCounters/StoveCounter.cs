using System;
using System.Linq;
using CodeMonkey.KitchenChaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    public class StoveCounter : Counter, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }

        public enum State
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
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _fryingTimer / _currentFryingRecipe.FryingMaxTime
                    });

                    if (_fryingTimer < _currentFryingRecipe.FryingMaxTime) return;

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentFryingRecipe.Output, this);
                    _burningTimer = 0f;

                    _currentBurningRecipe = GetBurningRecipeWithInput(GetKitchenObject().Data);

                    _state = State.Fried;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _burningTimer / _currentBurningRecipe.BurningMaxTime
                    });

                    if (_burningTimer < _currentBurningRecipe.BurningMaxTime) return;

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentBurningRecipe.Output, this);
                    _burningTimer = 0f;
                    _state = State.Burned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                    break;
                case State.Burned: break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject()))
                {
                    player.GetKitchenObject().SetAndTeleportToParent(this);
                    _currentFryingRecipe = GetFryingRecipeWithInput(GetKitchenObject().Data);
                    _state = State.Frying;
                    _fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _fryingTimer / _currentFryingRecipe.FryingMaxTime
                    });
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().Data))
                        {
                            GetKitchenObject().DestroySelf();
                            _state = State.Idle;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetAndTeleportToParent(player);
                    _state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
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
