import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";
import Button from "@mui/material/Button";
import Head from "next/head";

export default function Home() {
  return (
    <>
      <Head>
        <meta
          name="viewport"
          content="initial-scale=1, width=device-width"
        />
      </Head>
      <main>
        <h1>Home</h1>
        <Button
          variant="contained"
          color="primary">
          É a home
        </Button>
      </main>
    </>
  );
}
