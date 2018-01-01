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

/////////////////////////////////////////////////////////////////////////////////////////////////////////
/// This script contains the javascript component class for the study search panel

// Define and register the control type.
Type.registerNamespace('MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls');

/////////////////////////////////////////////////////////////////////////////////////////////////////////
// Constructor
MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView = function(element) { 
    MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView.initializeBase(this, [element]);
},

/////////////////////////////////////////////////////////////////////////////////////////////////////////
// Create the prototype for the control.
MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView.prototype = 
{
    initialize : function() {
        MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView.callBaseMethod(this, 'initialize');        
            
        this._OnLoadHandler = Function.createDelegate(this,this._OnLoad);
        this._OnSeriesListDoubleClickedHandler = Function.createDelegate(this,this._OnSeriesListDoubleClicked);
            
        Sys.Application.add_load(this._OnLoadHandler);
                 
    },
        
    dispose : function() {
        $clearHandlers(this.get_element());

        MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView.callBaseMethod(this, 'dispose');
            
        Sys.Application.remove_load(this._OnLoadHandler);
    },

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Events
        
    /// called whenever the page is reloaded or partially reloaded
    _OnLoad : function()
    {
        var serieslist = $find(this._SeriesListClientID);
        if (serieslist!=null)
        {
            serieslist.add_onClientRowDblClick(this._OnSeriesListDoubleClickedHandler);
        }
    },
        
    // called when the user double click on the series list
    _OnSeriesListDoubleClicked : function(src, event)
    {
        var serieslist = $find(this._SeriesListClientID);
        if (serieslist!=null)
        {
            var rows = serieslist.getSelectedRowElements();
            for(i=0; i<rows.length; i++)
            {
                var url = String.format('{0}?serverae={1}&studyuid={2}&seriesuid={3}', 
                            this._OpenSeriesPageUrl, 
                            this._getServerAE(rows[i]), 
                            this._getStudyUid(rows[i]), 
                            this._getSeriesUid(rows[i]));
                window.open(url);
            }    
        }
    },
        
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Private Methods
    _getServerAE : function (row)
    {
        return row.getAttribute('serverae');
    },
        
    _getStudyUid : function (row)
    {
        return row.getAttribute('studyuid');
    },
        
    _getSeriesUid: function (row)
    {
        return row.getAttribute('seriesuid');
    },

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Properties
    get_SeriesListClientID : function() {
        return this._SeriesListClientID;
    },

    set_SeriesListClientID: function(value) {
        this._SeriesListClientID = value;
        this.raisePropertyChanged('SeriesListClientID');
    },
        
    get_OpenSeriesPageUrl : function() {
        return this._OpenSeriesPageUrl;
    },
       
    set_OpenSeriesPageUrl : function(value) {
        this._OpenSeriesPageUrl = value;
        this.raisePropertyChanged('OpenSeriesPageUrl');
    }
},

// Register the class as a type that inherits from Sys.UI.Control.

MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView.registerClass('MatrixPACS.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls.SeriesGridView', Sys.UI.Control);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();