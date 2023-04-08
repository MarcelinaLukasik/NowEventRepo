import React from "react";
import lightbulb_1 from "../../images/icons/light_bulb_small.png";
import { useState, useEffect } from "react";
import EventInfoPanel from "../Main/EventInfoPanel";


function Assistance(props) {
  const [placeDetails, setPlaceDetails] = useState();

  useEffect(() => {
    fetchVerification();
  }, []);

  async function fetchVerification() {
    const res = await fetch(`/location/${props.eventId}/GetVerificationInfo`);
    const jsonResult = await res.json();
    setPlaceDetails(jsonResult);
    return jsonResult;
  }

  return (
    <div>
      <img src={lightbulb_1} alt="img" className="assistanceIcon" />
      {placeDetails && <EventInfoPanel placeDetails={placeDetails} />}
    </div>
  );
}

export default Assistance;
