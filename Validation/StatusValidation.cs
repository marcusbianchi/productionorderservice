using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using productionorderservice.Services.Interfaces;
using productionorderservice.Model;
using System.Collections.Generic;

namespace productionorderservice.Validation
{
    public enum stateEnum
    {
        created,
        active,
        inactive,
        paused,
        ended,
        waiting_approval,
        approved,
        reproved
    }
    public class StatusValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            ICollection<ConfiguredState> configuredStates = (ICollection<ConfiguredState>)value;
            bool statusValid = true;
            foreach (var state in configuredStates)
            {
                if (!belongsToEnum(state.state))
                    statusValid = false;
                foreach (var nextState in state.possibleNextStates)
                {
                    if (!belongsToEnum(nextState))
                        statusValid = false;
                }
            }
            if (!statusValid)
                return new ValidationResult("Some of The Status are Invalid!");
            return ValidationResult.Success;
        }

        private bool validateStateConfiguration(StateConfiguration stateConfiguration)
        {
            foreach (var state in stateConfiguration.states)
            {
                if (!belongsToEnum(state.state))
                    return false;
                foreach (var nextState in state.possibleNextStates)
                {
                    if (!belongsToEnum(nextState))
                        return false;
                }
            }
            return true;
        }

        private bool belongsToEnum(string state)
        {
            state = state.ToLower();
            stateEnum outEnum = stateEnum.active;
            return Enum.TryParse(state, out outEnum);
        }

    }
}