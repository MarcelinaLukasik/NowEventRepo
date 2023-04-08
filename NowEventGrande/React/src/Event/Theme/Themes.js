import formalImage from "../../images/themes/formal.jpg";
import spookyImage from "../../images/themes/spooky.jpg";
import tropicalImage from "../../images/themes/tropical.jpg";
import retroImage from "../../images/themes/retro.jpg";
import discoImage from "../../images/themes/disco.jpg";
import otherImage from "../../images/themes/other.jpg";

export const AllowedThemes = {
  Formal: "Formal",
  Spooky: "Spooky",
  Tropical: "Tropical",
  Retro: "Retro",
  Disco: "Disco",
  Other: "Other",
};

export const Themes = [
  { Name: AllowedThemes.Formal, Image: formalImage },
  { Name: AllowedThemes.Spooky, Image: spookyImage },
  { Name: AllowedThemes.Tropical, Image: tropicalImage },
  { Name: AllowedThemes.Retro, Image: retroImage },
  { Name: AllowedThemes.Disco, Image: discoImage },
  { Name: AllowedThemes.Other, Image: otherImage },
];
