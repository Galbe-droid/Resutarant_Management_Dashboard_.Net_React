import {Login} from "./pages/Login.tsx";
import {AuthProvider} from "./Providers/AuthProvider.tsx";
import {BrowserRouter} from "react-router-dom";

function App() {

  return (
      <AuthProvider>
        <BrowserRouter>
          <Login />
        </BrowserRouter>
      </AuthProvider>
  )
}

export default App
