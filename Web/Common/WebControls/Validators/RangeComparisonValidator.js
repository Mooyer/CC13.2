// License

// Copyright (c) 2013, MatrixPACS Inc.
// All rights reserved.
// http://www.MatrixPACS.ca
//
// This file is part of the MatrixPACS RIS/PACS open source project.
//
// The MatrixPACS RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The MatrixPACS RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the MatrixPACS RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

// End License

// This script defines the client-side validator extension class @@CLIENTID@@_ClientSideEvaludator
// to validate an input within a specified range and is greater or less than another input.
//
// This class defines how the validation is carried and what to do afterwards.


// derive this class from BaseClientValidator
ClassHelper.extend(@@CLIENTID@@_ClientSideEvaluator, BaseClientValidator);

//constructor
function @@CLIENTID@@_ClientSideEvaluator()
{
    BaseClientValidator.call(this, 
            '@@INPUT_CLIENTID@@',
            '@@INPUT_NORMAL_BKCOLOR@@',
            '@@INPUT_INVALID_BKCOLOR@@',
            '@@INPUT_NORMAL_BORDERCOLOR@@',
            '@@INPUT_INVALID_BORDERCOLOR@@', 
            '@@INPUT_NORMAL_CSS@@',
            '@@INPUT_INVALID_CSS@@',                                               
            '@@INVALID_INPUT_INDICATOR_CLIENTID@@'=='null'? null:document.getElementById('@@INVALID_INPUT_INDICATOR_CLIENTID@@'),
            '@@INVALID_INPUT_INDICATOR_TOOLTIP_CLIENTID@@'=='null'? null:document.getElementById('@@INVALID_INPUT_INDICATOR_TOOLTIP_CLIENTID@@'),
            '@@INVALID_INPUT_INDICATOR_TOOLTIP_CONTAINER_CLIENTID@@'=='null'? null:document.getElementById('@@INVALID_INPUT_INDICATOR_TOOLTIP_CONTAINER_CLIENTID@@'),
            '@@IGNORE_EMPTY_VALUE@@',
            '@@CONDITION_CHECKBOX_CLIENTID@@'=='null'? null:document.getElementById('@@CONDITION_CHECKBOX_CLIENTID@@'),
            @@VALIDATE_WHEN_UNCHECKED@@
    );
}


// override BaseClientValidator.OnEvaludate() 
// This function is called to evaluate the input
@@CLIENTID@@_ClientSideEvaluator.prototype.OnEvaluate = function()
{
    result = BaseClientValidator.prototype.OnEvaluate.call(this);
    
    if (result.OK==false)
    {
        return result;
    }
        
    compareCtrl = document.getElementById('@@COMPARE_INPUT_CLIENTID@@');
   
    result = new ValidationResult();
    if (this.input.value!=null && this.input.value!='')
    {
        controlValue = parseNumber(this.input.value);
        if (compareCtrl!=null && compareCtrl.value!='' &&  !isNaN(controlValue))
        {
            compareValue = parseNumber(compareCtrl.value);
            result.OK = controlValue >= @@MIN_VALUE@@ && controlValue<= @@MAX_VALUE@@ && controlValue @@COMPARISON_OP@@ compareValue;
            
            if (result.OK == false)
            {
                result.Message = '@@ERROR_MESSAGE@@';
                
            }
        }
        else
        {
            result.OK = false;
            result.Message = '@@ERROR_MESSAGE@@';
        }
    }   
    else
    {
          result.OK = false;  
    }
    
    
    
    return result;

};
