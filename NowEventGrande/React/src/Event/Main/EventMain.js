import SideBar from "../Utils/SideBar";
import React from "react";
import "../../styles/guests.css";
import { useLocation } from "react-router-dom";
import { Col } from "react-bootstrap";
import calendarIcon from "../../images/icons/list.png";
import { createContext } from "react";

const EventIdContext = createContext();

function EventMain() {
  const location = useLocation();
  const eventId = location.state.eventId;

  return (
    <div className="event">
      <div>
        <h1>New event</h1>
        <div className="row">
          <div className="Event-col-3">
            <EventIdContext.Provider value={eventId}>
              <div className="sidebar">
                <SideBar eventId={eventId} />
              </div>
            </EventIdContext.Provider>
          </div>

          <div className="Event-col-5">
            <br />
            <h3>
              Welcome to your new event! You can start planning by choosing one
              of the options in the menu.
            </h3>
          </div>
          <div className="Event-col-2">
            <Col cs={12} md={6} xl={6}>
              <img src={calendarIcon} alt="img" className="calendar" />
            </Col>
          </div>
        </div>
      </div>
    </div>
  );
}

export default EventMain;
export { EventIdContext };
