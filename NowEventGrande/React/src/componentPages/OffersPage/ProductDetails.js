import * as React from "react";
import { Container, Nav } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import "../../styles/Offers/ProductDetail.css";
import sample from "../../images/sample.jpg";
import { Divider, Grid, Table, TableBody,
  TableCell, TableContainer, TableRow, Typography,
} from "@mui/material";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Paper } from "@mui/material";
import { Link } from "react-router-dom";

export const ProductDetails = () => {
  const { id } = useParams();
  const [offer, setOffer] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    agent.Offers.productDetails(parseInt(id))
      .then((offer) => setOffer(offer))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, [id]);
  if (loading) return <LoadingComponent message="Loading offer..." />;
  return (
    <div class="event">
      <h1>Event details</h1>
      <Container className="container_offers">
        <Paper elevation={12}>
          <Grid container spacing={6} sx={{ mt: 10, p: 1 }}>
            <Grid item xs={4}>
              <img src={sample} alt={offer.name} style={{ width: "100%" }} />
            </Grid>
            <Grid item xs={8}>
              <Typography variant="h4" style={{ textAlign: "center" }}>
                {offer.name}
              </Typography>
              <Divider sx={{ mb: 2 }} />
              <TableContainer>
                <Table>
                  <TableBody>
                    <TableRow>
                      <TableCell>Type</TableCell>
                      <TableCell>{offer.type}</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>Size</TableCell>
                      <TableCell>{offer.size}</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>Event Start</TableCell>
                      <TableCell>{offer.eventStart}</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>Theme</TableCell>
                      <TableCell>{offer.theme}</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>Budget</TableCell>
                      <TableCell>{offer.budget}$</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>Address</TableCell>
                      <TableCell>{offer.address}</TableCell>
                    </TableRow>
                  </TableBody>
                  <Nav.Link as={Link} to={`/alloffers/offerToPrepare/${id}`}>
                    <button className="saveDate">Offer</button>
                  </Nav.Link>
                </Table>
              </TableContainer>
            </Grid>
          </Grid>
        </Paper>
      </Container>
    </div>
  );
};
