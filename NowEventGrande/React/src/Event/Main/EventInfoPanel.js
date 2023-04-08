import React from "react";
import "../../styles/event.css";

function EventInfoPanel(props) {
  return (
    <div className="infoPanel">
      <h5 className="infoNote">
        Here is some (hopefully) useful information about your event!
      </h5>
      <ol>
        {props.placeDetails.PlaceStatus && (
          <li>{props.placeDetails.PlaceStatus}</li>
        )}
        {props.placeDetails.EventStartStatus && (
          <li>{props.placeDetails.EventStartStatus}</li>
        )}
        {props.placeDetails.EventEndStatus && (
          <li>{props.placeDetails.EventEndStatus}</li>
        )}
      </ol>
    </div>
  );
}

export default EventInfoPanel;
