using System;

namespace BioSafeVaccineMonitor
{
    // ========== INTERFACE (Abstraction) ==========
    public interface IAlertable
    {
        void TriggerAlarm();
    }

    // ========== BASE CLASS (Inheritance, Encapsulation) ==========
    public abstract class MedicalStorage : IAlertable
    {
        private string _storageID;
        private double _currentTemp;

        public string StorageID
        {
            get { return _storageID; }
            private set { _storageID = value; }
        }

        public double CurrentTemp
        {
            get { return _currentTemp; }
            set
            {
                if (value < -100 || value > 50)
                {
                    throw new ArgumentException($"ERROR: Temperature {value}°C is out of realistic range (-100°C to 50°C).");
                }
                _currentTemp = value;
            }
        }

        public MedicalStorage(string id, double initialTemp)
        {
            StorageID = id;
            CurrentTemp = initialTemp;
        }

        public virtual bool IsTemperatureSafe()
        {
            return true;
        }

        public virtual void TriggerAlarm()
        {
            Console.WriteLine($"  ⚠ ALERT: Storage unit {StorageID} reports a temperature issue!");
        }

        public void SimulateDoorOpen(int seconds)
        {
            Console.WriteLine($"  🚪 Door opened for {seconds} seconds...");
            
            if (seconds > 30)
            {
                throw new InvalidOperationException($"  ❌ DOOR ALERT: Door open for {seconds} seconds exceeds the 30-second safety limit!");
            }
            else
            {
                Console.WriteLine($"  ✓ Door open for {seconds} seconds – within safe limit.");
            }
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"  📦 Unit: {StorageID} | Type: {this.GetType().Name} | Temp: {CurrentTemp}°C");
        }
    }

    // ========== SUBCLASS 1: UltraColdFreezer ==========
    public class UltraColdFreezer : MedicalStorage
    {
        private const double MIN_SAFE_TEMP = -80;
        private const double MAX_SAFE_TEMP = -60;

        public UltraColdFreezer(string id, double initialTemp) : base(id, initialTemp) { }

        public override bool IsTemperatureSafe()
        {
            bool isSafe = CurrentTemp >= MIN_SAFE_TEMP && CurrentTemp <= MAX_SAFE_TEMP;
            return isSafe;
        }

        public override void TriggerAlarm()
        {
            Console.WriteLine($"  🚨🚨 ULTRA-COLD FREEZER ALARM 🚨🚨");
            Console.WriteLine($"  Unit: {StorageID} | Current Temp: {CurrentTemp}°C");
            Console.WriteLine($"  Safe Range: {MIN_SAFE_TEMP}°C to {MAX_SAFE_TEMP}°C");
            Console.WriteLine($"  Action: IMMEDIATE inspection required!");
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"     Safe Range: {MIN_SAFE_TEMP}°C to {MAX_SAFE_TEMP}°C");
            Console.WriteLine($"     Status: {(IsTemperatureSafe() ? "✓ SAFE" : "⚠ UNSAFE")}");
        }
    }

    // ========== SUBCLASS 2: StandardFridge ==========
    public class StandardFridge : MedicalStorage
    {
        private const double MIN_SAFE_TEMP = 2;
        private const double MAX_SAFE_TEMP = 8;

        public StandardFridge(string id, double initialTemp) : base(id, initialTemp) { }

        public override bool IsTemperatureSafe()
        {
            bool isSafe = CurrentTemp >= MIN_SAFE_TEMP && CurrentTemp <= MAX_SAFE_TEMP;
            return isSafe;
        }

        public override void TriggerAlarm()
        {
            Console.WriteLine($"  🚨 STANDARD FRIDGE ALARM 🚨");
            Console.WriteLine($"  Unit: {StorageID} | Current Temp: {CurrentTemp}°C");
            Console.WriteLine($"  Safe Range: {MIN_SAFE_TEMP}°C to {MAX_SAFE_TEMP}°C");
            Console.WriteLine($"  Action: Check cooling system.");
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"     Safe Range: {MIN_SAFE_TEMP}°C to {MAX_SAFE_TEMP}°C");
            Console.WriteLine($"     Status: {(IsTemperatureSafe() ? "✓ SAFE" : "⚠ UNSAFE")}");
        }
    }

    // ========== MAIN PROGRAM WITH WHILE LOOP ==========
    class Program
    {
        static void Main(string[] args)
        {
            // TRY-CATCH-FINALLY block wraps the entire program
            try
            {
                Console.WriteLine("==========================================");
                Console.WriteLine("     BIO-SAFE VACCINE MONITOR SYSTEM     ");
                Console.WriteLine("==========================================\n");

                // Create storage units
                MedicalStorage freezer = new UltraColdFreezer("UC-001", -70);
                MedicalStorage fridge = new StandardFridge("SF-101", 5);
                
                Console.WriteLine("\n✅ Storage units initialized successfully!\n");

                // ========== WHILE LOOP FOR CONTINUOUS MONITORING ==========
                // This loop keeps the program running until user chooses to exit
                bool running = true;
                
                while (running)
                {
                    try
                    {
                        // Display menu
                        Console.WriteLine("\n========== MAIN MENU ==========");
                        Console.WriteLine("1. View all storage units status");
                        Console.WriteLine("2. Check temperature safety");
                        Console.WriteLine("3. Change temperature");
                        Console.WriteLine("4. Simulate door open");
                        Console.WriteLine("5. Trigger manual alarm test");
                        Console.WriteLine("6. Exit system");
                        Console.WriteLine("================================");
                        Console.Write("Enter your choice (1-6): ");
                        
                        string input = Console.ReadLine();
                        int choice;
                        
                        // Validate menu choice
                        if (!int.TryParse(input, out choice))
                        {
                            Console.WriteLine("❌ Invalid input. Please enter a number.");
                            continue; // Go back to menu
                        }
                        
                        // Handle user choice
                        switch (choice)
                        {
                            case 1: // View all units
                                Console.WriteLine("\n--- CURRENT STATUS ---");
                                freezer.ShowInfo();
                                Console.WriteLine();
                                fridge.ShowInfo();
                                break;
                                
                            case 2: // Check temperature safety
                                Console.WriteLine("\n--- TEMPERATURE SAFETY CHECK ---");
                                
                                // Check freezer
                                Console.Write($"Freezer {freezer.StorageID}: ");
                                if (!freezer.IsTemperatureSafe())
                                {
                                    Console.WriteLine("❌ UNSAFE");
                                    freezer.TriggerAlarm();
                                }
                                else
                                {
                                    Console.WriteLine("✓ SAFE");
                                }
                                
                                // Check fridge
                                Console.Write($"Fridge {fridge.StorageID}: ");
                                if (!fridge.IsTemperatureSafe())
                                {
                                    Console.WriteLine("❌ UNSAFE");
                                    fridge.TriggerAlarm();
                                }
                                else
                                {
                                    Console.WriteLine("✓ SAFE");
                                }
                                break;
                                
                            case 3: // Change temperature
                                Console.WriteLine("\n--- CHANGE TEMPERATURE ---");
                                Console.Write("Select unit (1: Freezer, 2: Fridge): ");
                                string unitChoice = Console.ReadLine();
                                
                                Console.Write("Enter new temperature (°C): ");
                                string tempInput = Console.ReadLine();
                                double newTemp;
                                
                                if (!double.TryParse(tempInput, out newTemp))
                                {
                                    Console.WriteLine("❌ Invalid temperature format.");
                                    break;
                                }
                                
                                // Apply temperature change to selected unit
                                if (unitChoice == "1")
                                {
                                    freezer.CurrentTemp = newTemp;
                                    Console.WriteLine($"✓ Freezer temperature updated to {newTemp}°C");
                                }
                                else if (unitChoice == "2")
                                {
                                    fridge.CurrentTemp = newTemp;
                                    Console.WriteLine($"✓ Fridge temperature updated to {newTemp}°C");
                                }
                                else
                                {
                                    Console.WriteLine("❌ Invalid unit selection.");
                                }
                                break;
                                
                            case 4: // Simulate door open
                                Console.WriteLine("\n--- DOOR OPEN SIMULATION ---");
                                Console.Write("Select unit (1: Freezer, 2: Fridge): ");
                                string doorUnit = Console.ReadLine();
                                
                                Console.Write("Enter seconds door was open: ");
                                string secondsInput = Console.ReadLine();
                                int seconds;
                                
                                if (!int.TryParse(secondsInput, out seconds))
                                {
                                    Console.WriteLine("❌ Invalid seconds format.");
                                    break;
                                }
                                
                                // Simulate door open on selected unit
                                if (doorUnit == "1")
                                {
                                    freezer.SimulateDoorOpen(seconds);
                                }
                                else if (doorUnit == "2")
                                {
                                    fridge.SimulateDoorOpen(seconds);
                                }
                                else
                                {
                                    Console.WriteLine("❌ Invalid unit selection.");
                                }
                                break;
                                
                            case 5: // Trigger manual alarm test
                                Console.WriteLine("\n--- MANUAL ALARM TEST ---");
                                Console.WriteLine("Testing freezer alarm:");
                                freezer.TriggerAlarm();
                                Console.WriteLine("\nTesting fridge alarm:");
                                fridge.TriggerAlarm();
                                break;
                                
                            case 6: // Exit
                                Console.WriteLine("\n🛑 Shutting down monitoring system...");
                                running = false; // This will exit the while loop
                                break;
                                
                            default:
                                Console.WriteLine("❌ Invalid choice. Please enter 1-6.");
                                break;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        // Handle validation errors inside the menu loop
                        Console.WriteLine($"\n❌ Validation Error: {ex.Message}");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle operation errors inside the menu loop
                        Console.WriteLine($"\n❌ Operation Error: {ex.Message}");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        // Handle unexpected errors
                        Console.WriteLine($"\n❌ Unexpected Error: {ex.Message}");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                } // END OF WHILE LOOP
            }
            catch (Exception ex)
            {
                // Catch any errors during initialization
                Console.WriteLine($"\n❌ Fatal Error: {ex.Message}");
            }
            finally
            {
                // FINALLY BLOCK - ALWAYS EXECUTES
                Console.WriteLine("\n==========================================");
                Console.WriteLine("  🔒 SYSTEM SHUTDOWN - Session Ended");
                Console.WriteLine("==========================================");
            }
        }
    }
}