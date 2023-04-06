import SideBar from "./SideBar";
import Carousel from "./Carousel";
import ProgressBar from "./ProgressBar";
import React from "react";
import { useLocation } from "react-router-dom";
import { useState } from "react";

function Details() {
  const location = useLocation();
  const eventId = location.state.EventId;
  const [fetchCurrentProgress, setFetchCurrentProgress] = useState(false);

  return (
    <div className="event">
      <div className="row">
        <div className="Event-col-12">
          <ProgressBar
            fetchCurrentProgress={fetchCurrentProgress}
            setFetchCurrentProgress={setFetchCurrentProgress}
          />
        </div>
      </div>
      <h1>Details</h1>

      <div className="row">
        <div className="Event-col-3">
          <div className="sidebar">
            <SideBar eventId={eventId} />
          </div>
        </div>
        <div className="Event-col-1"></div>
        <div className="Event-col-4">
          <Carousel />
        </div>
      </div>
    </div>
  );
}

export default Details;
