export default function Home() {
  const bkashPaymentHandler = async () => {
    try {
      // Use fetch instead of Axios
      const response = await fetch(
        "http://localhost:5000/" + "api/bkash/create",
        {
          method: "POST", // POST method
          headers: {
            "Content-Type": "application/json", // Assuming the server expects JSON
          },
          // Add any necessary body here, if needed
          body: JSON.stringify({
            mode: "0011",
            amount: 500, // Ensure `amount` is defined or passed into the component
            currency: "BDT",
            intent: "sale",
          }), // Empty body or whatever data needs to be sent
        }
      );

      // Check if response is OK (status code 200-299)
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const result = await response.json(); // Parse the JSON response

      // Handle the result
      if (result?.status) {
        window.location.href = result?.data?.data?.bkashURL;
      } else {
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="flex items-center justify-center mt-[100px]">
      <button
        className="bg-blue-500 text-white px-3 py-2 rounded-md"
        onClick={bkashPaymentHandler}
      >
        Pay With Bkash
      </button>
    </div>
  );
}
