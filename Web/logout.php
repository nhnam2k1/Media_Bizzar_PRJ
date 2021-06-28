<?php
    session_start();
    session_destroy();
    // Include URL for Login page to login again.
    header("Location: index.html");
?>