using Godot;
using System;

/// <summary>
/// A class that represents the player's animation state.
/// Used for carrying the references to both the boy and girl animations. Or in other words, its states
/// </summary>
public class PlayerState
{
    public string AnimationReferenceOne;
    public string AnimationReferenceTwo;
    public string CurrentAnimationReference;

    public PlayerState() : this("") { }

    public PlayerState(string referenceOne)
    {
        AnimationReferenceOne = referenceOne;
        AnimationReferenceTwo = "";
        CurrentAnimationReference = referenceOne;
    }

    public PlayerState(string referenceOne, string referenceTwo) : this(referenceOne)
    {
        AnimationReferenceTwo = referenceTwo;
    }

    public static implicit operator Godot.StringName(PlayerState state)
    {
        return state.CurrentAnimationReference;
    }

    /// <summary>
    /// Gets the current state of the animation
    /// </summary>
    /// <returns>a reference(by name) towards the applicable animation</returns>
    public string GetState()
    {
        return CurrentAnimationReference;
    }

    /// <summary>
    /// Switches the currently used state reference, with the other set reference
    /// </summary>
    public void SwitchStates()
    {
        if (CurrentAnimationReference == AnimationReferenceOne)
            CurrentAnimationReference = AnimationReferenceTwo;
        else
            CurrentAnimationReference = AnimationReferenceOne;
    }

    /// <summary>
    /// Sets the state to the first or second reference
    /// </summary>
    /// <param name="state">A bool that indicates the first or sencond state</param>
    public void SetState(bool state)
    {
        if (state)
            CurrentAnimationReference = AnimationReferenceTwo;
        else
            CurrentAnimationReference = AnimationReferenceOne;
    }
}