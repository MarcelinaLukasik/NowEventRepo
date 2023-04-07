import React from "react";
import { Outlet, NavLink } from "react-router-dom";
import "../styles/sidebar.css";

function SideBar(props) {
  const SideBarFeatures = [
    { Title: "Guest list", EndPointName: "guests" },
    { Title: "Budget", EndPointName: "budget" },
    { Title: "Location and date", EndPointName: "location" },
    { Title: "Make offer", EndPointName: "offer" },
    { Title: "Details (optional)", EndPointName: "details" },
    { Title: "Summary", EndPointName: "summary" },
  ];

  return (
    <div className="vertical-menu">
      <ul>
        {Array.from(SideBarFeatures).map((feature, i) => {
          return (
            <li key={i}>
              <NavLink
                to={{ pathname: `/event/${props.eventId}/${feature.EndPointName}` }}
                state={{ EventId: props.eventId }}
              >
                {feature.Title}
              </NavLink>
            </li>
          );
        })}
      </ul>
      <Outlet />
    </div>
  );
}

export default SideBar;
