import AppBar from "@mui/material/AppBar";
import Button from "@mui/material/Button";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Link from "next/link";

export default function Header() {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography
          variant="h6"
          component="div"
          sx={{ flexGrow: 1 }}>
          My Website
        </Typography>
        <Link
          href="/"
          color="inherit">
          <Button color="inherit">Home</Button>
        </Link>
        <Link
          href="/products"
          color="inherit">
          <Button color="inherit">Products</Button>
        </Link>
        <Link
          href="/packings"
          color="inherit">
          <Button color="inherit">Packings</Button>
        </Link>
        <Link
          href="/recipes"
          color="inherit">
          <Button color="inherit">Recipes</Button>
        </Link>
      </Toolbar>
    </AppBar>
  );
}